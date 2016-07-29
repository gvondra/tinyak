Imports System.ServiceModel

<ServiceContract([Namespace]:="urn:service.tinyak.net/User/v1", Name:="User")>
Public Interface IUser

    <OperationContract()>
    Function IsEmailAddressAvailable(ByVal strEmailAddress As String) As Boolean

End Interface
