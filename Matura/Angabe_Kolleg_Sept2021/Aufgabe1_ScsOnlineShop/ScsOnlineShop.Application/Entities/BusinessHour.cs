namespace ScsOnlineShop.Application.Entities
{
    public class BusinessHour : OpeningHour
    {
        private BusinessHour() { }
        public BusinessHour(OpeningHour o)
            : base(dayOfWeek: o.DayOfWeek, hourFrom: o.HourFrom, hourTo: o.HourTo, store: o.Store)
        { }
    }
}
