namespace WebRISCV
{
    public class CircularIdGenerator
    {
        private string[] colors;
        private int currentIndex;

        public CircularIdGenerator(string[] inputColors)
        {
            colors = inputColors;
            currentIndex = 0;
        }

        public string Current 
        {
            get { return colors[currentIndex]; }
        }

        public string GetNextId()
        {
            string nextId = colors[currentIndex];
            currentIndex = (currentIndex + 1) % colors.Length;
            return nextId;
        }
    }
}
