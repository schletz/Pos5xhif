namespace ScsOnlineShop.Application.Entities
{
    public class DeliveryHour : OpeningHour
    {
        private DeliveryHour() { }
        public DeliveryHour(OpeningHour o)
            : base(dayOfWeek: o.DayOfWeek, hourFrom: o.HourFrom, hourTo: o.HourTo, store: o.Store)
        { }

    }
}
