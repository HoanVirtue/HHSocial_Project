namespace Clone_Main_Project_0710.Methods
{
    public class DateTimeMethod
    {
        public static string CalTimeCurrentDifferent(DateTime? dateTime)
        {
            if(dateTime != null)
            {
                TimeSpan timeDiff = (TimeSpan)(DateTime.Now - dateTime);
                
                int days = timeDiff.Days;
                int hours = timeDiff.Hours;
                int minutes = timeDiff.Minutes;
                if(days > 0) {
                    return days + " ngày trước";
                } else if(hours > 0) {
                    return hours + " giờ trước";
                } else if(minutes > 0) {
                    return minutes + " phút trước";
                } else {
                    return "1 phút trước";
                }
            }
            return "";
        }
    }
}