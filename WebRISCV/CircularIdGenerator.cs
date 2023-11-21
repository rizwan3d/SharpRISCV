using System.Globalization;

namespace WebRISCV
{
    public class CircularIdGenerator
    {
        private List<string> items;
        private int currentIndex;

        public CircularIdGenerator(List<string> inputItems)
        {
            items = inputItems;
            Reset();
        }

        public string Current 
        {
            get {
                if (items.Count == 0) return string.Empty;
                return items[currentIndex]; 
            }
        }

        public string GetNextId()
        {
            string nextId = items[currentIndex];
            currentIndex = (currentIndex + 1) % items.Count;
            return nextId;
        }

        public void Reset()
        {
            currentIndex = 0;
        }
    }
}
