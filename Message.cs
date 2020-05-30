namespace tcpip_ex01_dotnetcore
{
    [System.Serializable]
    class Message
    {
        public int id { get; set; }
        public string content { get; set; }

        public Message(int id, string content)
        {
            this.id = id;
            this.content = content;
        }

        public Message() {}

        public override string ToString()
        {
            string idToPrint = this.id.ToString("D2");
            return $"{{ id:{idToPrint}, content:\"{this.content}\" }}";
        }
    }
}
