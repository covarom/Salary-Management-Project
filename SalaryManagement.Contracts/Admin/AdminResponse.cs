namespace SalaryManagement.Contracts.Admin
{
    public class AdminResponse {
        string Id;
        string Name;
        string PhoneNumber;
        string Image;
        string email;
        string Username;

        public AdminResponse(string id , string name, string phone, string image,string email,string username){
            this.Id=id;
            this.Name=name;
            this.PhoneNumber=phone;
            this.Image=image;
            this.email=email;
            this.Username=username;
        }
       }

}
