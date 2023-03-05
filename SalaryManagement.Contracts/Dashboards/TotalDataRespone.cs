namespace SalaryManagement.Contracts.Dashboards
{
    public record TotalNum
    {
        public int totalEmps;
        public int totalCompanies;
        public int totalContracts;
        public int totalPayslips;

        public TotalNum(int totalEmp, int Company, int Contract, int Payslip)
        {
            this.totalEmps = totalEmp;
            this.totalCompanies = Company;
            this.totalContracts = Contract;
            this.totalPayslips = Payslip;
        }
    }
}