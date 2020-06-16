using System;

namespace course.Entities
{
    class LogRecord
    {
        public string UserName { get; set; }
        public DateTime Insert { get; set; }

        public LogRecord(string userName, DateTime insert)
        {
            UserName = userName;
            Insert = insert;
        }

        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LogRecord)) 
            {
                return false;
            }
            LogRecord other = obj as LogRecord;
            return UserName.Equals(other.UserName);
        }
    }
}