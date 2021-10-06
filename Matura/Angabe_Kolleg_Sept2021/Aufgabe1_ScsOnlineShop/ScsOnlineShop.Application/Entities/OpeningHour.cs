using System;

namespace ScsOnlineShop.Application.Entities
{
    public class OpeningHour
    {
        protected OpeningHour() { }
        public OpeningHour(DayOfWeek dayOfWeek, int hourFrom, int hourTo, ActiveStore store)
        {
            DayOfWeek = dayOfWeek;
            HourFrom = hourFrom;
            HourTo = hourTo;
            StoreId = store.Id;
            Store = store;
        }

        public int Id { get; private set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int HourFrom { get; set; }
        public int HourTo { get; set; }
        public int StoreId { get; set; }
        public virtual ActiveStore Store { get; set; } = default!;
    }
}
