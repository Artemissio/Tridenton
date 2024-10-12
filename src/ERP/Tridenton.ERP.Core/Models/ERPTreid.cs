namespace Tridenton.ERP.Core;

public sealed record ERPTreid : Treid
{
    private const char ERPSystemIdSeparator = '/';

    /// <summary>
    /// ERP System Id
    /// </summary>
    public readonly string ERPSystemId;
    
    /// <summary>
    /// Initializes a new instance of <see cref="ERPTreid"/>
    /// </summary>
    /// <param name="partition">Partition</param>
    /// <param name="account">Account number</param>
    /// <param name="servicesGroup">Services group</param>
    /// <param name="service">Service</param>
    /// <param name="resourceType">Resource type</param>
    /// <param name="erpSystemId">Resource Id</param>
    /// <param name="resourceId">Resource Id</param>
    private ERPTreid(string partition, string account, string servicesGroup, string service, string resourceType, string erpSystemId, string resourceId = Constants.Wildcard)
        : base(partition, account, servicesGroup, service, resourceType, resourceId)
    {
        ERPSystemId = erpSystemId;
    }

    public Treid ToTreid()
    {
        return new Treid(
            partition: Partition,
            account: Account,
            servicesGroup: ServicesGroup,
            service: Service,
            resourceType: ResourceType,
            resourceId: $"{ERPSystemId}{ERPSystemIdSeparator}{ResourceId}");
    }

    public static ERPTreid FromTreid(Treid treid)
    {
        var resourceIdSplit = treid.ResourceId
            .Split(ERPSystemIdSeparator)
            .AsSpan();

        if (resourceIdSplit.Length != 2)
        {
            throw new FormatException("Resource Id is not in the correct format.");
        }

        return new ERPTreid(
            partition: treid.Partition,
            account: treid.Account,
            servicesGroup: treid.ServicesGroup,
            service: treid.Service,
            resourceType: treid.ResourceType,
            erpSystemId: resourceIdSplit[0],
            resourceId: resourceIdSplit[1]);
    }
}