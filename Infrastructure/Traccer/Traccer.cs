using System.Diagnostics;

namespace Work360.Infrastructure.Traccer
{


    public static class Tracing
    {
        public static readonly ActivitySource ActivitySource =
            new ActivitySource("Work360.API");
    }
}