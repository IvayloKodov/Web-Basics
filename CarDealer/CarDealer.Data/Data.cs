namespace CarDealer.Data
{
    public class Data
    {
        private static CarDealerContext carDealerContext;

        public static CarDealerContext Context()
        {
            return carDealerContext ?? (carDealerContext = new CarDealerContext());
        }
    }
}