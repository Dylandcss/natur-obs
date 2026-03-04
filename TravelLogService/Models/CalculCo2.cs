namespace TravelLogService.Models;

    public static class CalculCo2
    {
        private const double CarEmission = 0.220;
        private const double BusEmission = 0.110;
        private const double TrainEmission = 0.030;
        private const double PlaneEmission = 0.259;
        private const double ZeroEmission = 0.000;

        public static double Calculate(double distanceKm, TravelMode mode)
        {
            if (distanceKm <= 0)
                throw new ArgumentException("La distance doit être positive et plus grand que 0.");

            double emissionPerKm = mode switch
            {
                TravelMode.Walking => ZeroEmission,
                TravelMode.Bike => ZeroEmission,
                TravelMode.Car => CarEmission,
                TravelMode.Bus => BusEmission,
                TravelMode.Train => TrainEmission,
                TravelMode.Plane => PlaneEmission,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), "Mode de transport invalide.")
            };

            return Math.Round(distanceKm * emissionPerKm, 2);
        }
    }