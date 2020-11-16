namespace g_auto.Models
{
    public class ServiceToInfo
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ServiceInfoId { get; set; }
        public Service Service { get; set; }
        public ServiceInfo ServiceInfo { get; set; }

    }
}