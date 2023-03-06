namespace SalaryManagement.Contracts.Dashboards
{
    public class KeyValue{
        public string name;
        public int value;
         public KeyValue(string key, int value)
        {
            this.name = key;
            this.value = value;
        }
    }
}