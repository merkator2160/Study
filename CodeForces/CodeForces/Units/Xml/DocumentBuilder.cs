using CodeForces.Units.XmlUnite.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CodeForces.Units.XmlUnite
{
    /// <summary>
    /// базовый класс для строителей документов для Молотка и Яндекса
    /// </summary>
    public abstract class DocumentBuilder : IDisposable
    {
        protected const int SMARTPHONES_CATEGORY_ID = 146;
        protected const int PADS_CATEGORY_ID = 4;
        protected const int CHEKLY_CATEGORY_ID = 245;
        protected const int GOLOVNIE_USTR_CATEGORY_ID = 992;

        protected const string DOCUMENT_TYPE_NAME = "yml_catalog";
        protected const string DOCUMENT_TYPE_SYSTEM_ID = "shops.dtd";



        protected const string ROOT_FIELD_DATE_ATTRIBUTE_NAME = "date";

        protected const string SHOP_FIELD_NAME = "shop";
        protected const string NAME_FIELD_NAME = "name";
        protected const string COMPANY_FIELD_NAME = "company";
        protected const string URL_FIELD_NAME = "url";


        protected virtual string CategoriesFieldName => "categories";
        protected virtual string CategoryFieldName { get { return "category"; } }
        protected const string CATEGORY_ID_FIELD_NAME = "categoryId";
        protected const string CATEGORY_FIELD_ID_ATTRIBUTE_NAME = "id";
        protected const string CATEGORY_FIELD_PARENT_ATTRIBUTE_NAME = "parentId";

        protected const string CURRENCIES_FIELD_NAME = "currencies";
        protected const string CURRENCY_FIELD_NAME = "currency";
        protected const string CURRENCY_FIELD_ID_FIELD_NAME = "id";
        protected const string CURRENCY_FIELD_ID_FIELD_CONTENT = "RUR";
        protected const string CURRENCY_FIELD_RATE_FIELD_NAME = "rate";
        protected const string CURRENCY_FIELD_RATE_FIELD_CONTENT = "1";
        protected const string DESCRIPTION_FIELD_NAME = "description";

        protected const string OFFERS_FIELD_NAME = "offers";
        protected const string OFFER_FIELD_PARAM_FIELD_NAME = "param";
        protected const string OFFER_FIELD_NAME_ATTRIBUTE_NAME = "name";

        protected virtual string OfferFieldName => "offer";
        protected const string OFFER_FIELD_URL_FIELD_NAME = "url";
        protected const string OFFER_FIELD_ID_ATTRIBUTE_NAME = "id";
        protected const string OFFER_FIELD_AVAILABLE_ATTRIBUTE_NAME = "available";
        protected const string OFFER_FIELD_TYPE_ATTRIBUTE_NAME = "type";
        protected const string OFFER_FIELD_TYPE_ATTRIBUTE_CONTENT = "vendor.model";
        protected const string OFFER_FIELD_OLDPRICE_FIELD_NAME = "oldprice";
        protected const string OFFER_FIELD_PRICE_FIELD_NAME = "price";
        protected const string OFFER_FIELD_CURRENCY_ID_FIELD_NAME = "currencyId";
        protected const string OFFER_FIELD_CATEGORY_ID_FIELD_NAME = "categoryId";
        protected const string OFFER_FIELD_PICTURE_FIELD_NAME = "picture";
        protected const string OFFER_FIELD_NAME_FIELD_NAME = "name";
        protected const string OFFER_FIELD_VENDOR_FIELD_NAME = "vendor";
        protected const string OFFER_FIELD_DESCRIPTION_NAME = "description";
        protected const string OFFER_FIELD_TYPE_PREFIX_FIELD_NAME = "typePrefix";
        protected const string OFFER_FIELD_MODEL_FIELD_NAME = "model";

        private const string VENDOR_PID1 = "285";
        private const string VENDOR_PID2 = "21";
        private const string VENDOR_PID3 = "223";
        protected const string MODEL_PID = "280";
        private const string VENDOR_NONAME = "Другие";


        protected static readonly Dictionary<string, int> categoriesWithPhysAndVirtualIds = new Dictionary<string, int>();
        protected string[] excludedPropertyIds = new[] { "21", "282" };
        private HashSet<string> _excludedPhysIds;
        private List<VirtualCategoryInfoWithProductsCount> _virtualCategoryInfos;


        public DocumentBuilder(CategoryServiceHelper categoryServiceHelper, DocumentBuilderSettings settings)
        {
            CategoryServiceHelper = categoryServiceHelper;

            Settings = settings;

            OffersTempFilePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
            FinalTempFilePath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            CategoriesForSave = new HashSet<DocumentBuilderVirtualCategory>();
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public DocumentBuilderSettings Settings { get; set; }
        public bool IsValid
        {
            get
            {
                return Settings != null && !string.IsNullOrEmpty(Settings.FileName)
                        && !string.IsNullOrEmpty(Settings.CompanyName) && !string.IsNullOrEmpty(Settings.ShopName)
                        && Settings.ShopUrl != null;
            }
        }
        public HashSet<PropertyValueWithName> PropertiesWithNames { get; set; }
        protected XmlWriter OffersXmlTextWriter { get; set; }
        protected string OffersTempFilePath { get; private set; }
        protected string FinalTempFilePath { get; private set; }
        public MainDocumentBuilder ParentBuilder { protected get; set; }
        protected CategoryServiceHelper CategoryServiceHelper { get; private set; }
        protected HashSet<DocumentBuilderVirtualCategory> CategoriesForSave { get; private set; }
        protected virtual string RootFieldName => "yml_catalog";


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public void Save()
        {
            if (IsValid)
            {
                var xmlsettings = new XmlReaderSettings();
                xmlsettings.ConformanceLevel = ConformanceLevel.Document;
                xmlsettings.DtdProcessing = DtdProcessing.Ignore;
                xmlsettings.XmlResolver = null;

                using (var reader = XmlReader.Create(OffersTempFilePath, xmlsettings))
                {
                    //создаем начало документа
                    using (var writer = CreateDocumentStart())
                    {
                        //записываем товары
                        WriteProductsNodes(writer, reader);

                        //записываем конец документа
                        CreateDocumentEnd(writer);

                        writer.Flush();
                    }
                }

                if (string.IsNullOrEmpty(Settings.FileName))
                    return;

                var directoryName = Path.GetDirectoryName(Settings.FileName);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }


                File.Copy(FinalTempFilePath, Settings.FileName, true);
                File.Delete(FinalTempFilePath);
                File.Delete(OffersTempFilePath);
            }
        }
        public void AddPhysIds(IEnumerable<string> physIds, string seoId)
        {
            if (physIds != null)
            {
                var existCategorySetting = Settings.IncludedCategories.SingleOrDefault(s => s.CategorySeoId == seoId);
                if (existCategorySetting != null)
                {
                    if (_virtualCategoryInfos == null)
                        _virtualCategoryInfos = new List<VirtualCategoryInfoWithProductsCount>();

                    var info = new VirtualCategoryInfoWithProductsCount()
                    {
                        MaxProductsCount = existCategorySetting.MaxProductsCount,
                    };
                    info.PhysicalIds.AddRange(physIds);

                    _virtualCategoryInfos.Add(info);
                }
            }
        }
        public void AddExcludedPhysIds(IEnumerable<string> physIds)
        {
            if (physIds != null)
            {
                if (_excludedPhysIds == null)
                    _excludedPhysIds = new HashSet<string>();

                _excludedPhysIds.AddRange(physIds);
            }
        }
        public IEnumerable<string> GetPhysIds()
        {
            return _virtualCategoryInfos != null
                ? _virtualCategoryInfos.SelectMany(ic => ic.PhysicalIds).Distinct()
                : null;
        }
        public bool IsMaxProductsCountActive()
        {
            var result = false;

            if (Settings.MaxProductsCount != null && _virtualCategoryInfos != null)
            {
                result = _virtualCategoryInfos.Sum(vci => vci.ProcessedProductsCount) >= Settings.MaxProductsCount;
            }

            return result;
        }
        protected static IEnumerable<PhotoInfo> GetParamNodesWithImages(ProductItem productItem)
        {
            var result = new List<PhotoInfo>();

            if (productItem != null && productItem.ProductImages != null && productItem.ProductImages.Count > 1)
            {
                var upperBound = productItem.ProductImages.Count <= 8 ? productItem.ProductImages.Count : 8;

                for (var i = 1; i < upperBound; i++)
                {
                    if (productItem.ProductImages[i] == null)
                        continue;

                    var photoName = String.Format(CultureInfo.InvariantCulture, "Фото {0}", i + 1);

                    result.Add(new PhotoInfo()
                    {
                        Address = productItem.ProductImages[i].OriginalImageAddress,
                        Name = photoName
                    });
                }
            }

            return result;
        }
        protected static string GetProductModel(ProductItemBase product, string modelPid)
        {
            string result = null;

            if (product != null && product.Properties != null && product.Properties.Any())
            {
                result = product.Properties.Where(p => p.PropertyId == modelPid).Select(p => p.PropertyValueTitle).FirstOrDefault();

                if (String.IsNullOrWhiteSpace(result))
                {
                    result = product.ProductName;
                }
            }

            return result;
        }
        protected static VendorInfo GetProductVendor(ProductItemBase product)
        {
            var retVal = new VendorInfo();

            if (product != null && product.Properties != null && product.Properties.Any())
            {
                retVal = product.Properties.Where(p => p.PropertyId == VENDOR_PID1).Select(p => new VendorInfo()
                {
                    VendorName = p.PropertyValueTitle,
                    VendorValueId = p.ValueId
                }).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(retVal.VendorName))
                {
                    retVal = product.Properties.Where(p => p.PropertyId == VENDOR_PID2).Select(p => new VendorInfo()
                    {
                        VendorName = p.PropertyValueTitle,
                        VendorValueId = p.ValueId
                    }).FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(retVal.VendorName))
                    {
                        retVal = product.Properties.Where(p => p.PropertyId == VENDOR_PID3).Select(p => new VendorInfo()
                        {
                            VendorName = p.PropertyValueTitle,
                            VendorValueId = p.ValueId
                        }).FirstOrDefault();

                        if (string.IsNullOrWhiteSpace(retVal.VendorName))
                        {
                            retVal = new VendorInfo()
                            {
                                VendorName = VENDOR_NONAME,
                                VendorValueId = "Fail"
                            };
                        }
                    }
                }
            }

            return retVal;
        }
        /// <summary>
        /// выполняем проверки на возможность обработки текущего продукта и заносим его в дерево категорий.
        /// Если добавить в дерево не удалось, то продукт обрабатывать нельзя. В этом случае возращаем null
        /// </summary>
        /// <param name="productItem"></param>
        /// <returns></returns>
        protected int? CanWriteCurrentProductAndGetVirtualCategoryId(ProductItem productItem)
        {
            int? result = null;

            if (productItem != null && !SkipCurrentProduct(productItem))
            {
                int? virtualCategoryId = CategoryServiceHelper.GetVirtualCategoryId(productItem, categoriesWithPhysAndVirtualIds);

                if (virtualCategoryId != null)
                {
                    var currentCategory = ParentBuilder.AllCategories.SingleOrDefault(c => c.Id == virtualCategoryId.Value);
                    if (currentCategory != null)
                    {
                        result = virtualCategoryId.Value;

                        SaveCurrentCategoryPath(currentCategory);
                    }
                }
            }

            return result;
        }
        private void SaveCurrentCategoryPath(DocumentBuilderVirtualCategory currentCategory)
        {
            if (!CategoriesForSave.Any(c => c.Id == currentCategory.Id))
            {
                CategoriesForSave.Add(currentCategory);

                var id = currentCategory.ParentId;
                while (id.HasValue)
                {
                    var parentCategory = ParentBuilder.AllCategories.SingleOrDefault(c => c.Id == id.Value);
                    if (parentCategory != null)
                    {
                        id = parentCategory.ParentId;
                        CategoriesForSave.Add(parentCategory);
                    }
                    else
                    {
                        id = null;
                    }
                }
            }
        }
        protected string GetCategoryPath(int virtualCategoryId, string separator = " > ")
        {
            var categories = new List<string>();

            while (virtualCategoryId != 0)
            {
                var existCategory = ParentBuilder.AllCategories.SingleOrDefault(c => c.Id == virtualCategoryId);
                if (existCategory != null)
                {
                    categories.Add(existCategory.Name);
                    virtualCategoryId = existCategory.ParentId.HasValue ? existCategory.ParentId.Value : 0;
                }
                else
                {
                    virtualCategoryId = 0;
                }
            }

            return string.Join(separator, Enumerable.Reverse(categories));
        }
        protected IEnumerable<long> GetCategoryIdPath(int virtualCategoryId)
        {
            var categories = new List<long>();

            while (virtualCategoryId != 0)
            {
                var existCategory = ParentBuilder.AllCategories.SingleOrDefault(c => c.Id == virtualCategoryId);
                if (existCategory != null)
                {
                    categories.Add(existCategory.Id);
                    virtualCategoryId = existCategory.ParentId ?? 0;
                }
                else
                {
                    virtualCategoryId = 0;
                }
            }

            return categories;
        }
        protected string GetProductVendorString(ProductItem product)
        {
            string result = null;

            if (product != null && product.Properties != null && product.Properties.Any())
            {
                result = product.Properties.Where(p => p.PropertyId == VENDOR_PID1).Select(p => p.PropertyValueTitle).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = product.Properties.Where(p => p.PropertyId == VENDOR_PID2).Select(p => p.PropertyValueTitle).FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        result = product.Properties.Where(p => p.PropertyId == VENDOR_PID3).Select(p => p.PropertyValueTitle).FirstOrDefault();

                        if (string.IsNullOrWhiteSpace(result))
                            result = VENDOR_NONAME;
                    }
                }
            }

            return result;
        }
        protected IEnumerable<PropertyValueWithName> GetDescriptionProperties(ProductItem productItem)
        {
            var result = new List<PropertyValueWithName>();

            if (PropertiesWithNames != null)
            {
                var productPropertyValues = PropertiesWithNames.Where(v => productItem.Properties
                    .Any(p => p.PropertyId == v.PropertyId && p.ValueId == v.ValueId))
                    .Distinct(new PropertyValueWithNameEqualityComparer());

                result.AddRange(productPropertyValues);
            }

            return result;
        }
        protected abstract string GetDescription(ProductItem productItem);
        protected abstract bool ProcessProduct(ProductItem productItem);
        public abstract XmlWriter CreateDocumentStart();
        public abstract void CreateDocumentEnd(XmlWriter writer);
        public virtual void CreateOffersDocumentStart()
        {
            var xmlWriterSettigns = CreateXmlWriterSettings();

            OffersXmlTextWriter = XmlWriter.Create(OffersTempFilePath, xmlWriterSettigns);

            OffersXmlTextWriter.WriteStartDocument();
            OffersXmlTextWriter.WriteDocType(DOCUMENT_TYPE_NAME, null, DOCUMENT_TYPE_SYSTEM_ID, null);

            OffersXmlTextWriter.WriteStartElement(OFFERS_FIELD_NAME);
        }
        protected virtual XmlWriterSettings CreateXmlWriterSettings()
        {
            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;
            xmlWriterSettings.Encoding = Encoding.GetEncoding("windows-1251");
            xmlWriterSettings.Indent = true;

            return xmlWriterSettings;
        }
        public virtual void CreateOffersDocumentEnd()
        {
            if (OffersXmlTextWriter == null) return;

            OffersXmlTextWriter.WriteEndElement();
            OffersXmlTextWriter.WriteEndDocument();

            OffersXmlTextWriter.Flush();
            OffersXmlTextWriter.Close();
        }
        protected virtual string GetProductPropertiesFromDescriptionLastPart(ProductItem productItem, IEnumerable<string> excludedPropertyIds = null)
        {
            var builder = new StringBuilder();

            if (PropertiesWithNames != null)
            {
                var productPropertyValues = PropertiesWithNames.Where(v => productItem.Properties
                    .Any(p => p.PropertyId == v.PropertyId && p.ValueId == v.ValueId))
                    .Distinct(new PropertyValueWithNameEqualityComparer());

                if (excludedPropertyIds != null && excludedPropertyIds.Any())
                {
                    var finalPropertiesList = new List<PropertyValueWithName>();

                    foreach (var propertyValueWithName in productPropertyValues)
                    {
                        if (!excludedPropertyIds.Contains(propertyValueWithName.PropertyId))
                        {
                            finalPropertiesList.Add(propertyValueWithName);
                        }
                    }

                    productPropertyValues = finalPropertiesList;
                }

                foreach (var property in productPropertyValues)
                {
                    if (builder.Length > 0)
                        builder.Append("<br/>");

                    builder.Append(HttpUtility.HtmlEncode(property.Name));
                    builder.Append(": ");
                    builder.Append(HttpUtility.HtmlEncode(property.PropertyValueTitle));
                }
            }

            return builder.ToString();
        }
        public virtual void WriteXmlProduct(ProductItem productItem)
        {
            if (productItem == null)
                return;

            if (ProcessProduct(productItem))
            {
                IncrementProcessedProductsCount(productItem.CategoryId);
            }
        }
        public virtual void InitBuilder()
        {

        }
        protected virtual void WriteCategories(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(CategoriesFieldName);
            foreach (var virtualCategoryInfoDto in CategoriesForSave.OrderBy(c => c.Id))
            {
                xmlWriter.WriteStartElement(CategoryFieldName);
                xmlWriter.WriteAttributeString(CATEGORY_FIELD_ID_ATTRIBUTE_NAME, virtualCategoryInfoDto.Id.ToString(CultureInfo.InvariantCulture));

                if (virtualCategoryInfoDto.ParentId != null)
                {
                    xmlWriter.WriteAttributeString(CATEGORY_FIELD_PARENT_ATTRIBUTE_NAME, virtualCategoryInfoDto.ParentId.ToString());
                }
                xmlWriter.WriteValue(virtualCategoryInfoDto.Name);

                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        }
        protected virtual void WriteProductsNodes(XmlWriter writer, XmlReader reader)
        {
            if (writer == null)
                return;

            reader.MoveToContent();
            writer.WriteNode(reader, true);
        }
        private bool SkipCurrentProduct(ProductItem productItem)
        {
            var result = false;

            //если делаем выгрузку по категориям и продукт не содержится в нужной категории
            if (_virtualCategoryInfos != null)
            {
                var allPhysIds = _virtualCategoryInfos.SelectMany(ic => ic.PhysicalIds).Distinct();
                if (!allPhysIds.Contains(productItem.CategoryId))
                {
                    result = true;
                }

                //проверяем количество обработанных товаров
                if (!result)
                {
                    var currentPhysCategory = _virtualCategoryInfos.Where(pi => pi.PhysicalIds.Contains(productItem.CategoryId)).Select(pi => pi);
                    if (currentPhysCategory.Any(pc => pc.HasProductsCountRestriction && pc.IsProcessedGreatThanMax))
                    {
                        result = true;
                    }
                }
            }

            //если категория есть в списке исключеных
            if (!result)
            {
                if (_excludedPhysIds != null && _excludedPhysIds.Contains(productItem.CategoryId))
                {
                    result = true;
                }
            }

            //проверяем на общее кол-во обработанных товаров
            if (!result)
            {
                result = IsMaxProductsCountActive();
            }

            return result;
        }
        /// <summary>
        /// если в настройках указано максимальное кол-во товаров, то увеличиваем счетчик обработанного количества
        /// </summary>
        private void IncrementProcessedProductsCount(string physCategoryId)
        {
            if (_virtualCategoryInfos != null)
            {
                var infos = _virtualCategoryInfos.Where(pi => pi.PhysicalIds.Contains(physCategoryId)).Select(pi => pi);
                if (infos.Any(i => i.MaxProductsCount != null))
                {
                    foreach (var processedProductsInfo in infos.Where(i => i.MaxProductsCount != null))
                    {
                        processedProductsInfo.ProcessedProductsCount++;
                    }
                }
            }
        }


        // IDisposible ////////////////////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (OffersXmlTextWriter != null)
                {
                    OffersXmlTextWriter.Dispose();
                    OffersXmlTextWriter = null;
                }
            }
        }
    }
}
