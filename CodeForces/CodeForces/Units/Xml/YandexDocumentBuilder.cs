using CodeForces.Units.XmlUnite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace CodeForces.Units.XmlUnite
{
    [Guid("D935CB40-EEBD-4F2B-B9DC-E3D6DF8CADB4")]
    public class YandexDocumentBuilder : DocumentBuilder
    {


        public YandexDocumentBuilder(CategoryServiceHelper categoryServiceHelper, DocumentBuilderSettings settings)
            : base(categoryServiceHelper, settings)
        {

        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private static string GetTypePrefix(int virtualCategoryId)
        {
            var retVal = string.Empty;

            switch (virtualCategoryId)
            {
                case PADS_CATEGORY_ID:
                    retVal = "Планшет";
                    break;
                case SMARTPHONES_CATEGORY_ID:
                    retVal = "Смартфон";
                    break;
                case CHEKLY_CATEGORY_ID:
                    retVal = "Чехол";
                    break;
            }

            return retVal;
        }
        private static string GetProductModel(ProductItem productItem)
        {
            if (productItem == null)
                return string.Empty;

            var result = string.Empty;

            try
            {
                var firstSpaceIndex = productItem.ProductName.IndexOf(' ');
                if (firstSpaceIndex != -1)
                {
                    var secondSpaceIndex = productItem.ProductName.IndexOf(' ', firstSpaceIndex + 1);
                    if (secondSpaceIndex != -1)
                    {
                        var firstCommaIndex = productItem.ProductName.IndexOf(',');
                        if (firstCommaIndex != -1)
                        {
                            result = productItem.ProductName.Substring(secondSpaceIndex + 1, firstCommaIndex - secondSpaceIndex - 1);
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = "NoModel";
            }


            return result;
        }
        protected override string GetDescription(ProductItem productItem)
        {
            var builder = new StringBuilder();

            string description = GetProductPropertiesFromDescriptionLastPart(productItem, excludedPropertyIds);
            if (!string.IsNullOrEmpty(description))
            {
                builder.Append(description.Replace("<br/>", " "));
            }

            return builder.ToString();
        }
        protected override bool ProcessProduct(ProductItem productItem)
        {
            var result = true;

            var documentType = YandexDocumentType.Simple;
            var partnerType = PartnerType.None;

            if (Settings != null && Settings.State != null && Settings.State is YandexDocumentSettings)
            {
                var yandexSettings = Settings.State as YandexDocumentSettings;
                documentType = yandexSettings.DocumentType;
                partnerType = yandexSettings.PartnerType;
            }

            var virtualCategoryId = CanWriteCurrentProductAndGetVirtualCategoryId(productItem);
            if (virtualCategoryId != null)
            {
                OffersXmlTextWriter.WriteStartElement(OfferFieldName);
                OffersXmlTextWriter.WriteAttributeString(OFFER_FIELD_ID_ATTRIBUTE_NAME, productItem.ItemId);


                if (documentType == YandexDocumentType.VendorModel)
                {
                    OffersXmlTextWriter.WriteAttributeString(OFFER_FIELD_TYPE_ATTRIBUTE_NAME, OFFER_FIELD_TYPE_ATTRIBUTE_CONTENT);
                }


                if (productItem.DetailProductUrl != null)
                {
                    var absolutePath = new Uri(Settings.ShopUrl, productItem.DetailProductUrl).ToString();

                    if (absolutePath.Length <= 512)
                    {
                        OffersXmlTextWriter.WriteElementString(OFFER_FIELD_URL_FIELD_NAME, absolutePath);
                    }
                }

                var pictures = new List<string>();
                if (productItem.ProductImages != null && productItem.ProductImages.Any())
                {
                    foreach (var productImage in productItem.ProductImages)
                    {
                        if (productImage != null && productImage.OriginalImageAddress.Length <= 512)
                        {
                            pictures.Add(productImage.OriginalImageAddress);
                            if (partnerType == PartnerType.None)
                            {
                                break;
                            }
                        }
                    }
                }

                if (documentType == YandexDocumentType.Blizko && pictures.Any())
                {
                    OffersXmlTextWriter.WriteElementString(OFFER_FIELD_PICTURE_FIELD_NAME, pictures.First());
                }

                //цена товара без скидки
                var oldPrice = productItem.Price.Price;
                //цена товара с учетом скидки
                var currentPrice = productItem.Price.Price - productItem.Discount.Amount;

                if (oldPrice != currentPrice)
                {
                    OffersXmlTextWriter.WriteElementString(OFFER_FIELD_OLDPRICE_FIELD_NAME, oldPrice.ToString("F2", CultureInfo.InvariantCulture));
                }

                var productPrice = currentPrice.ToString("F2", CultureInfo.InvariantCulture);
                OffersXmlTextWriter.WriteElementString(OFFER_FIELD_PRICE_FIELD_NAME, productPrice);

                OffersXmlTextWriter.WriteElementString(OFFER_FIELD_CURRENCY_ID_FIELD_NAME, "RUR");
                OffersXmlTextWriter.WriteElementString(OFFER_FIELD_CATEGORY_ID_FIELD_NAME, virtualCategoryId.Value.ToString(CultureInfo.InvariantCulture));


                if (documentType != YandexDocumentType.Blizko && pictures.Any())
                {
                    if (partnerType == PartnerType.None)
                    {
                        OffersXmlTextWriter.WriteElementString(OFFER_FIELD_PICTURE_FIELD_NAME, pictures.First());
                    }
                    else
                    {
                        foreach (var picture in pictures)
                        {
                            OffersXmlTextWriter.WriteElementString(OFFER_FIELD_PICTURE_FIELD_NAME, picture);
                        }
                    }

                }


                if (documentType == YandexDocumentType.VendorModel)
                {
                    //достаем тип товарногно предложения. Планшет, смартфон и т.п
                    var typePrefix = GetTypePrefix(virtualCategoryId.Value);
                    if (!string.IsNullOrWhiteSpace(typePrefix))
                    {
                        OffersXmlTextWriter.WriteElementString(OFFER_FIELD_TYPE_PREFIX_FIELD_NAME, typePrefix);
                    }
                }

                if (documentType == YandexDocumentType.Simple || documentType == YandexDocumentType.Blizko)
                {
                    //добавляем название
                    OffersXmlTextWriter.WriteElementString(OFFER_FIELD_NAME_FIELD_NAME, productItem.ProductName);
                }


                var vendorName = GetProductVendorString(productItem);
                //добавлем вендора
                OffersXmlTextWriter.WriteElementString(OFFER_FIELD_VENDOR_FIELD_NAME, vendorName);


                //добавляем модель
                if (documentType == YandexDocumentType.VendorModel)
                {
                    string model = GetProductModel(productItem);
                    OffersXmlTextWriter.WriteElementString(OFFER_FIELD_MODEL_FIELD_NAME, model);
                }

                //описание
                if (documentType != YandexDocumentType.VendorModel)
                {
                    var description = GetDescription(productItem);
                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        OffersXmlTextWriter.WriteElementString(OFFER_FIELD_DESCRIPTION_NAME, description);
                    }
                }
                else
                {
                    var properties = GetDescriptionProperties(productItem);

                    foreach (var property in properties)
                    {
                        OffersXmlTextWriter.WriteStartElement(OFFER_FIELD_PARAM_FIELD_NAME);
                        OffersXmlTextWriter.WriteAttributeString(OFFER_FIELD_NAME_FIELD_NAME, property.Name);
                        OffersXmlTextWriter.WriteValue(property.PropertyValueTitle);
                        OffersXmlTextWriter.WriteEndElement();
                    }
                }


                if (documentType != YandexDocumentType.Blizko)
                {
                    OffersXmlTextWriter.WriteStartElement(OFFER_FIELD_PARAM_FIELD_NAME);
                    OffersXmlTextWriter.WriteAttributeString("name", "Состояние");
                    OffersXmlTextWriter.WriteValue("новый");
                    OffersXmlTextWriter.WriteEndElement();
                }

                OffersXmlTextWriter.WriteEndElement();
            }
            else
            {
                result = false;
            }

            return result;
        }
        public override XmlWriter CreateDocumentStart()
        {
            var xmlsettings = CreateXmlWriterSettings();

            var xmlWriter = XmlWriter.Create(FinalTempFilePath, xmlsettings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteDocType(DOCUMENT_TYPE_NAME, null, DOCUMENT_TYPE_SYSTEM_ID, null);

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            xmlWriter.WriteStartElement(RootFieldName);
            xmlWriter.WriteAttributeString(ROOT_FIELD_DATE_ATTRIBUTE_NAME, currentDate);

            xmlWriter.WriteStartElement(SHOP_FIELD_NAME);

            xmlWriter.WriteElementString(NAME_FIELD_NAME, Settings.ShopName);
            xmlWriter.WriteElementString(COMPANY_FIELD_NAME, Settings.CompanyName);
            xmlWriter.WriteElementString(URL_FIELD_NAME, Settings.ShopUrl.ToString());

            xmlWriter.WriteStartElement(CURRENCIES_FIELD_NAME);
            xmlWriter.WriteStartElement(CURRENCY_FIELD_NAME);
            xmlWriter.WriteAttributeString(CURRENCY_FIELD_ID_FIELD_NAME, CURRENCY_FIELD_ID_FIELD_CONTENT);
            xmlWriter.WriteAttributeString(CURRENCY_FIELD_RATE_FIELD_NAME, CURRENCY_FIELD_RATE_FIELD_CONTENT);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            //заполняем категории
            WriteCategories(xmlWriter);

            return xmlWriter;
        }
        public override void CreateDocumentEnd(XmlWriter writer)
        {
            if (writer == null)
                return;

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }
}