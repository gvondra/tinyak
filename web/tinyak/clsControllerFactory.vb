Public Class clsControllerFactory
    Inherits DefaultControllerFactory

    Public Overrides Function CreateController(requestContext As RequestContext, controllerName As String) As IController
        Dim objType As Type

        objType = GetControllerType(requestContext, controllerName)
        If objType.IsSubclassOf(GetType(clsControllerBase)) Then
            Return CType(Activator.CreateInstance(objType), IController)
        Else
            Return MyBase.CreateController(requestContext, controllerName)
        End If
    End Function

End Class
