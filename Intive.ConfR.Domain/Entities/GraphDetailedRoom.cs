using System.Collections.Generic;

namespace Intive.ConfR.Domain.Entities
{
    public class GraphDetailedRoom
    {
        public string Id { get; set; }
        public string DeletedDateTime { get; set; }
        public bool AccountEnabled { get; set; }
        public string AgeGroup {get; set;}
        public List<string> BusinessPhones { get; set; }
        public string City { get; set; }
        public string CreatedDateTime { get; set; }
        public string CompanyName { get; set; }
        public string ConsentProviderForMinor { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string DisplayName { get; set; }
        public string EmployeeId { get; set; }
        public string FaxNumber { get; set; }
        public string GivenName { get; set; }
        public List<string> ImAddresses { get; set; }
        public string IsResourceAccount { get; set; }
        public string JobTitle { get; set; }
        public string LegalAgeGroupClassification { get; set; }
        public string Mail { get; set; }
        public string MailNickname { get; set; }
        public string MobilePhone { get; set; }
        public string OnPremisesDistinguishedName { get; set; }
        public string OfficeLocation { get; set; }
        public string OnPremisesDomainName { get; set; }
        public string OnPremisesImmutableId { get; set; }
        public string OnPremisesLastSyncDateTime { get; set; }
        public string OnPremisesSecurityIdentifier { get; set; }
        public string OnPremisesSamAccountName { get; set; }
        public string OnPremisesSyncEnabled { get; set; }
        public string OnPremisesUserPrincipalName { get; set; }
        public List<string> OtherMails { get; set; }
        public string PasswordPolicies { get; set; }
        public string PasswordProfile { get; set; }
        public string PostalCode { get; set; }
        public string PreferredDataLocation { get; set; }
        public string PreferredLanguage { get; set; }
        public List<string> ProxyAddresses { get; set; }
        public string RefreshTokensValidFromDateTime { get; set; }
        public string ShowInAddressList { get; set; }
        public string SignInSessionsValidFromDateTime { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string Surname { get; set; }
        public string UsageLocation { get; set; }
        public string UserPrincipalName { get; set; }
        public string ExternalUserState { get; set; }
        public string ExternalUserStateChangeDateTime { get; set; }
        public string UserType { get; set; }
        public List<Licenses> AssignedLicenses { get; set; }
        public List<AssignedPlans> AssignedPlans { get; set; }
        public List<string> DeviceKeys { get; set; }
        public PremisesExtensionAttributes OnPremisesExtensionAttributes { get; set; }
        public List<string> OnPremisesProvisioningErrors { get; set; }
        public List<ProvisionedPlans> ProvisionedPlans { get; set; }
    }

    public class Licenses
    {
        public List<string> DisabledPlans { get; set; }
        public string SkuId { get; set; }
    }

    public class AssignedPlans
    {
        public string AssignedDateTime { get; set; }
        public string CapabilityStatus { get; set; }
        public string Service { get; set; }
        public string ServicePlanId { get; set; }
    }

    public class ProvisionedPlans
    {
        public string CapabilityStatus { get; set; }
        public string ProvisioningStatus { get; set; }
        public string Service { get; set; }
    }

    public class PremisesExtensionAttributes
    {
        public string ExtensionAttribute1 { get; set; }
        public string ExtensionAttribute2 { get; set; }
        public string ExtensionAttribute3 { get; set; }
        public string ExtensionAttribute4 { get; set; }
        public string ExtensionAttribute5 { get; set; }
        public string ExtensionAttribute6 { get; set; }
        public string ExtensionAttribute7 { get; set; }
        public string ExtensionAttribute8 { get; set; }
        public string ExtensionAttribute9 { get; set; }
        public string ExtensionAttribute10 { get; set; }
        public string ExtensionAttribute11 { get; set; }
        public string ExtensionAttribute12 { get; set; }
        public string ExtensionAttribute13 { get; set; }
        public string ExtensionAttribute14 { get; set; }
        public string ExtensionAttribute15 { get; set; }
    }
}
