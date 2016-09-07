<DataContract(Name:="WorkItemState", Namespace:="urn:tinyak.net/api/v1")>
Public Enum enumWorkItemState As Int16
    <EnumMember> [New] = 0
    <EnumMember> [Approved] = 1
    <EnumMember> [Committed] = 2
    <EnumMember> [Complete] = 3
    <EnumMember> [Rejected] = 4
End Enum
