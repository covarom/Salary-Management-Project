namespace SalaryManagement.Contracts.Admin
{
    public class AdminResponse {
        public string AdminId { get; set; } = null!;
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image {get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }

        public AdminResponse(string id, string? name, string? phoneNumber, string? image, string? email, string? username)
        {
            AdminId = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Image = image;
            Email = email;
            Username = username;
        }

        public AdminResponse()
        {
        }



        /*  public AdminResponse(string id , string name, string phone, string image,string email,string username){
              this.Id=id;
              this.Name=name;
              this.PhoneNumber=phone;
              this.Image=image;
              this.email=email;
              this.Username=username;
          }*/



    }

}
