namespace Sapper.Model
{
    public class Record
    {
        public Record(Time recordTime)
        {
            Time = recordTime;
        }

        public Record()
        {
            Time = default;
        }

        public Time Time { get; private set; }

        public bool TryUpdate(Time recordTime)
        {
            Time defaultTime = default;

            if (Time.Minutes == defaultTime.Minutes)
            {
                if (Time.Seconds == defaultTime.Seconds)
                {
                    Time = recordTime;
                    return true;
                }
                                  
            }

            if (Time.Minutes >= recordTime.Minutes)
            {
                if (Time.Seconds > recordTime.Seconds)
                {
                    Time = recordTime;
                    return true;
                }                              
            }

            return false;
        }

        public void Update(Time recordTime)
        {
            Time = recordTime;
        }
    }
}