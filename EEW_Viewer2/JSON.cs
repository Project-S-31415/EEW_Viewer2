using System.Collections.Generic;

namespace EEW_Viewer2
{
    public class NIEDJsonClass
    {
        public string Message { get; set; }
        public string Report_Time { get; set; }
        public string Region_name { get; set; }
        public string Longitude { get; set; }
        public string Is_cancel { get; set; }
        public string Depth { get; set; }
        public string Calcintensity { get; set; }
        public string Is_final { get; set; }
        public string Is_traning { get; set; }
        public string Latitude { get; set; }
        public string Magunitude { get; set; }
        public string Report_num { get; set; }
        public string Alertflg { get; set; }
        public string Is_training { get; set; }
        public string Origin_time { get; set; }
    }

    public class Title
    {
        public int Code { get; set; }
        public string String { get; set; }
        public string Detail { get; set; }
    }

    public class Source
    {
        public int Code { get; set; }
        public string String { get; set; }
    }

    public class Status
    {
        public string Code { get; set; }
        public string String { get; set; }
        public string Detail { get; set; }
    }

    public class AnnouncedTime
    {
        public string String { get; set; }
        public int UnixTime { get; set; }
        public string RFC1123 { get; set; }
    }

    public class OriginTime
    {
        public string String { get; set; }
        public int UnixTime { get; set; }
        public string RFC1123 { get; set; }
    }

    public class Type
    {
        public int Code { get; set; }
        public string String { get; set; }
        public string Detail { get; set; }
    }

    public class Depth
    {
        public int Int { get; set; }
        public string String { get; set; }
        public int Code { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public Depth Depth { get; set; }
    }

    public class Magnitude
    {
        public double Float { get; set; }
        public string String { get; set; }
        public string LongString { get; set; }
        public int Code { get; set; }
    }

    public class Epicenter
    {
        public int Code { get; set; }
        public string String { get; set; }
        public int Rank2 { get; set; }
        public string String2 { get; set; }
    }

    public class Accuracy
    {
        public Epicenter Epicenter { get; set; }
        public Depth Depth { get; set; }
        public Magnitude Magnitude { get; set; }
        public int NumberOfMagnitudeCalculation { get; set; }
    }

    public class Hypocenter
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsAssumption { get; set; }
        public Location Location { get; set; }
        public Magnitude Magnitude { get; set; }
        public Accuracy Accuracy { get; set; }
        public bool IsSea { get; set; }
    }

    public class MaxIntensity
    {
        public string From { get; set; }
        public string To { get; set; }
        public string String { get; set; }
        public string LongString { get; set; }
    }

    public class WarnForecast
    {
        public Hypocenter Hypocenter { get; set; }
        public List<string> District { get; set; }
        public List<string> LocalAreas { get; set; }
        public List<string> Regions { get; set; }
    }

    public class Reason
    {
        public int Code { get; set; }
        public string String { get; set; }
    }

    public class Change
    {
        public int Code { get; set; }
        public string String { get; set; }
        public Reason Reason { get; set; }
    }

    public class Option
    {
        public Change Change { get; set; }
    }

    public class Intensity
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Description { get; set; }
    }

    public class Arrival
    {
        public bool Flag { get; set; }
        public string Condition { get; set; }
        public string Time { get; set; }
    }

    public class Forecast
    {
        public Intensity Intensity { get; set; }
        public bool Warn { get; set; }
        public Arrival Arrival { get; set; }
    }

    public class IedredJsonClass
    {
        public string ParseStatus { get; set; }
        public Title Title { get; set; }
        public Source Source { get; set; }
        public Status Status { get; set; }
        public AnnouncedTime AnnouncedTime { get; set; }
        public OriginTime OriginTime { get; set; }
        public string EventID { get; set; }
        public Type Type { get; set; }
        public int Serial { get; set; }
        public Hypocenter Hypocenter { get; set; }
        public MaxIntensity MaxIntensity { get; set; }
        public bool Warn { get; set; }
        public WarnForecast WarnForecast { get; set; }
        public Option Option { get; set; }
        public List<Forecast> Forecast { get; set; }
        public string OriginalText { get; set; }
    }
    public class Tokens_JSON
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessSecret { get; set; }
    }
}
