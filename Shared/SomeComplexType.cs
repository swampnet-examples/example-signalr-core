using System;

namespace Shared
{
    public class SomeComplexType
    {
        private static int _id;

        public SomeComplexType()
        {
            Id = ++_id;
            Timestamp = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
