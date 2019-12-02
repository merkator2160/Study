namespace CodeForces.Units.Xml.Models
{
    public class YandexDocumentSettings
    {
        public YandexDocumentSettings()
        {
            PartnerType = PartnerType.None;
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public YandexDocumentType DocumentType { get; set; }
        public PartnerType PartnerType { get; set; }
    }
}