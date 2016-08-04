Imports System.ServiceModel

<ServiceContract()>
Public Interface ISessionService

    <OperationContract()>
    Function [Get](ByVal objId As Guid) As clsSession

    <OperationContract>
    Function Create() As clsSession

    <OperationContract()>
    Sub Save(ByVal objSession As clsSession)
End Interface
