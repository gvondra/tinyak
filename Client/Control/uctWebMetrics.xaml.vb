Imports System.Data
Public Class uctWebMetrics
    Private m_objWebMetricsVm As clsWebMetricsVM

    Public Sub Load()
        Dim objLoad As LoadDataDelegate

        m_objWebMetricsVm = New clsWebMetricsVM
        DataContext = m_objWebMetricsVm

        objLoad = New LoadDataDelegate(AddressOf LoadData)
        objLoad.BeginInvoke(Nothing, objLoad)
    End Sub

    Private Delegate Sub LoadDataDelegate()
    Private Sub LoadData()
        Dim colWebMetrics As List(Of clsWebMetrics)
        Dim objWebMetrics As clsWebMetrics
        Dim dtData As DataTable
        Dim drwData As DataRow
        Dim objSetData As SetDataDelegate
        Dim datValue As Date

        Try
            colWebMetrics = clsWebMetrics.Get(New clsSettings, winMain.SessionId, Date.Now.AddMonths(-3))

            dtData = New DataTable
            dtData.Columns.Add("Url", GetType(String))
            dtData.Columns.Add("Controller", GetType(String))
            dtData.Columns.Add("Action", GetType(String))
            dtData.Columns.Add("Timestamp", GetType(Date))
            dtData.Columns.Add("Duration", GetType(Double))
            dtData.Columns.Add("ContentEncoding", GetType(String))
            dtData.Columns.Add("ContentLength", GetType(Integer))
            dtData.Columns.Add("ContentType", GetType(String))
            dtData.Columns.Add("Method", GetType(String))
            dtData.Columns.Add("RequestType", GetType(String))
            dtData.Columns.Add("TotalBytes", GetType(Integer))
            dtData.Columns.Add("UrlReferrer", GetType(String))
            dtData.Columns.Add("UserAgent", GetType(String))
            dtData.Columns.Add("Parameters", GetType(String))
            dtData.Columns.Add("StatusCode", GetType(Integer))
            dtData.Columns.Add("StatusDescription", GetType(String))

            If colWebMetrics IsNot Nothing Then
                For Each objWebMetrics In colWebMetrics
                    drwData = dtData.NewRow

                    drwData("Url") = objWebMetrics.Url
                    drwData("Controller") = objWebMetrics.Controller
                    drwData("Action") = objWebMetrics.Action
                    datValue = objWebMetrics.Timestamp
                    Date.SpecifyKind(datValue, DateTimeKind.Utc)
                    drwData("Timestamp") = datValue.ToLocalTime
                    If objWebMetrics.Duration.HasValue Then
                        drwData("Duration") = objWebMetrics.Duration.Value
                    Else
                        drwData("Duration") = DBNull.Value
                    End If
                    drwData("ContentEncoding") = objWebMetrics.ContentEncoding
                    If objWebMetrics.ContentLength.HasValue Then
                        drwData("ContentLength") = objWebMetrics.ContentLength.Value
                    Else
                        drwData("ContentLength") = DBNull.Value
                    End If
                    drwData("ContentType") = objWebMetrics.ContentType
                    drwData("Method") = objWebMetrics.Method
                    drwData("RequestType") = objWebMetrics.RequestType
                    If objWebMetrics.TotalBytes.HasValue Then
                        drwData("TotalBytes") = objWebMetrics.TotalBytes.Value
                    Else
                        drwData("TotalBytes") = DBNull.Value
                    End If
                    drwData("UrlReferrer") = objWebMetrics.UrlReferrer
                    drwData("UserAgent") = objWebMetrics.UserAgent
                    drwData("Parameters") = objWebMetrics.Parameters

                    If objWebMetrics.StatusCode.HasValue Then
                        drwData("StatusCode") = objWebMetrics.StatusCode.Value
                    Else
                        drwData("StatusCode") = DBNull.Value
                    End If
                    drwData("StatusDescription") = objWebMetrics.StatusDescription

                    dtData.Rows.Add(drwData)
                Next
            End If

            objSetData = New SetDataDelegate(AddressOf SetData)
            Dispatcher.Invoke(objSetData, dtData)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Delegate Sub SetDataDelegate(ByVal dtData As DataTable)
    Private Sub SetData(ByVal dtData As DataTable)
        m_objWebMetricsVm.Data = dtData
    End Sub
End Class
