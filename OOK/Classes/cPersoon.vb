Imports Newtonsoft.Json.Linq

Public Class cPersoon
#Region "Properties"


    Private _id As Long = -1        '-1 is not set
    Private _uuid As String

    Private _username As String = "-1"
    Private _firstname As String
    Private _lastname As String
    Private _initialen As String
    Private _email As String
    Private _voornamen As String    'not implemented door Onderwijs Online Webservice
    Private _Geslacht As String     'not implemented door Onderwijs Online Webservice
    Private _created_at As String '2015-06-16 092525
    Private _updated_at As String ' 2021-06-04 140129
    Private _foreign_id As String
    Private _deleted_at As String 'null
    Private _organisation As String
    Private _Function As String
    Private _phone As String
    Private _mobile As String
    Private _date_of_birth As String
    Private _place_of_birth As String
    Private _linkedin As String

    Private _twitter As String
    Private _code As String
    Private _description As String
    Private _lastname_prefix As String
    Private _credentials_sent_at As String
    Private _credentials_sent_by As String
    Private _online_status As String
    Private _urkund_email As String
    Private _fullname As String '[ Administrator 
    Private _id_hashed As String 'rXD51DbE
    Private _name As String 'Administrator
    Private _isSuperUser As Boolean 'true

    Private _PrimaireLocatieCode As String = ""
    Private _PrimaireLocatieNaam As String = ""
    Private _Teams As New Dictionary(Of String, String)

    Public Property Uuid As String
        Get
            Return _uuid
        End Get
        Set(value As String)
            _uuid = value
        End Set
    End Property

    Public Property Username As String
        Get
            Return _username
        End Get
        Set(value As String)
            _username = value
        End Set
    End Property

    Public Property Firstname As String
        Get
            Return _firstname
        End Get
        Set(value As String)
            If value = "" And _firstname <> "" Then Exit Property '
            _firstname = value
        End Set
    End Property



    Public Property Roepnaam As String
        Get
            Return _firstname
        End Get
        Set(value As String)
            If value = "" And _firstname <> "" Then Exit Property
            _firstname = value
        End Set
    End Property

    Public Property Lastname As String
        Get
            Return _lastname
        End Get
        Set(value As String)
            If value = "" And _lastname <> "" Then Exit Property
            _lastname = value
        End Set
    End Property
    Public Property Achternaam As String
        Get
            Return _lastname
        End Get
        Set(value As String)
            If value = "" And _lastname <> "" Then Exit Property
            _lastname = value
        End Set
    End Property

    Public Property Initialen As String
        Get
            Return _initialen
        End Get
        Set(value As String)
            If value = "" And _initialen <> "" Then Exit Property
            _initialen = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
        End Set
    End Property


    Public Property Created_at As String
        Get
            Return _created_at
        End Get
        Set(value As String)
            _created_at = value
        End Set
    End Property

    Public Property Updated_at As String
        Get
            Return _updated_at
        End Get
        Set(value As String)
            _updated_at = value
        End Set
    End Property

    Public Property Foreign_id As String
        Get
            Return _foreign_id
        End Get
        Set(value As String)
            _foreign_id = value
        End Set
    End Property

    Public Property Deleted_at As String
        Get
            Return _deleted_at
        End Get
        Set(value As String)
            _deleted_at = value
        End Set
    End Property

    Public Property Organisation As String
        Get
            Return _organisation
        End Get
        Set(value As String)
            _organisation = value
        End Set
    End Property

    Public Property [Function] As String
        Get
            Return _Function
        End Get
        Set(value As String)
            _Function = value
        End Set
    End Property

    Public Property Phone As String
        Get
            Return _phone
        End Get
        Set(value As String)
            _phone = value
        End Set
    End Property

    Public Property Mobile As String
        Get
            Return _mobile
        End Get
        Set(value As String)
            _mobile = value
        End Set
    End Property

    Public Property Mobiel_Nummer As String
        Get
            Return _mobile
        End Get
        Set(ByVal value As String)
            'checks op het telefoonnummer doen
            value = Replace(value, "-", "") 'opschonen nummer
            value = Replace(value, "(", "")
            value = Replace(value, ")", "")
            value = Replace(value, " ", "")

            If Left(value, 1) = "6" Then    'begint met een 6, wijzigen naar 06
                value = "0" & value
            End If
            If IsNothing(value) Then
                Exit Property
            End If
            If value.Length < 10 Then
                Throw New Exception("Telefoon nummer te kort")
                Exit Property
            End If
            If IsDBNull(value) Then
                value = ""
            End If
            _mobile = value

        End Set
    End Property
    Public Property Date_of_birth As String
        Get
            Return _date_of_birth
        End Get
        Set(value As String)
            _date_of_birth = value
        End Set
    End Property

    Public Property geboorteDatum As String
        Get
            Return Format(_date_of_birth, "YYYY-MM-DD")
        End Get
        Set(value As String)
            _date_of_birth = value
        End Set
    End Property

    Public Property Geslacht As String
        Get
            Return _Geslacht
        End Get
        Set(ByVal value As String)
            value = LCase(value)
            If Left(value, 1) = "v" Then
                value = "Vrouw"
            End If

            If Left(value, 1) = "m" Then
                value = "Man"
            End If

            _Geslacht = value
        End Set
    End Property
    Public Property Place_of_birth As String
        Get
            Return _place_of_birth
        End Get
        Set(value As String)
            _place_of_birth = value
        End Set
    End Property

    Public Property Linkedin As String
        Get
            Return _linkedin
        End Get
        Set(value As String)
            _linkedin = value
        End Set
    End Property

    Public Property Twitter As String
        Get
            Return _twitter
        End Get
        Set(value As String)
            _twitter = value
        End Set
    End Property

    Public Property Code As String
        Get
            Return _code
        End Get
        Set(value As String)
            _code = value
        End Set
    End Property

    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    Public Property Lastname_prefix As String
        Get
            Return _lastname_prefix
        End Get
        Set(value As String)
            _lastname_prefix = value
        End Set
    End Property
    Public Property Tussenvoegsels As String
        Get
            Return _lastname_prefix
        End Get
        Set(value As String)
            _lastname_prefix = value
        End Set
    End Property

    Public Property Credentials_sent_at As String
        Get
            Return _credentials_sent_at
        End Get
        Set(value As String)
            _credentials_sent_at = value
        End Set
    End Property

    Public Property Credentials_sent_by As String
        Get
            Return _credentials_sent_by
        End Get
        Set(value As String)
            _credentials_sent_by = value
        End Set
    End Property

    Public Property Online_status As String
        Get
            Return _online_status
        End Get
        Set(value As String)
            _online_status = value
        End Set
    End Property

    Public Property Urkund_email As String
        Get
            Return _urkund_email
        End Get
        Set(value As String)
            _urkund_email = value
        End Set
    End Property

    Public Property Fullname As String
        Get
            Return _fullname
        End Get
        Set(value As String)
            _fullname = value
        End Set
    End Property

    Public Property Id_hashed As String
        Get
            Return _id_hashed
        End Get
        Set(value As String)
            _id_hashed = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property IsSuperUser As Boolean
        Get
            Return _isSuperUser
        End Get
        Set(value As Boolean)
            _isSuperUser = value
        End Set
    End Property

    Public Property Id As Long
        Get
            Return _id
        End Get
        Set(value As Long)
            _id = value
        End Set
    End Property
    'Public Property OOid As Long
    '    Get
    '        Return _id
    '    End Get
    '    Set(value As Long)
    '        _id = value
    '    End Set
    'End Property
    Public Property Voornamen As String
        Get
            Return _voornamen
        End Get
        Set(value As String)
            _voornamen = value
        End Set
    End Property

    Public Property PrimaireLocatieCode As String
        Get
            Return _PrimaireLocatieCode
        End Get
        Set(value As String)
            _PrimaireLocatieCode = value
        End Set
    End Property

    Public Property PrimaireLocatieNaam As String
        Get
            Return _PrimaireLocatieNaam
        End Get
        Set(value As String)
            _PrimaireLocatieNaam = value
        End Set
    End Property

    Public Property Teams As Dictionary(Of String, String)
        Get
            Return _Teams
        End Get
        Set(value As Dictionary(Of String, String))
            _Teams = value
        End Set
    End Property
#End Region
    Public Overridable Function getUserData(jUser As JObject) As Boolean
        Try

            'functie leest het JuserObject uit naar de verschillende velden
            Me.Id = SafeGetJsonData("id", jUser)
            Me.Username = SafeGetJsonData("username", jUser)
            Me.Firstname = SafeGetJsonData("firstname", jUser)
            Me.Lastname = SafeGetJsonData("lastname", jUser)
            Me.Email = SafeGetJsonData("email", jUser)
            Me.Created_at = SafeGetJsonData("created_at", jUser)
            Me.Updated_at = SafeGetJsonData("updated_at", jUser)
            Me.Foreign_id = SafeGetJsonData("foreign_id", jUser)
            Me.Deleted_at = SafeGetJsonData("deleted_at", jUser)
            Me.Organisation = SafeGetJsonData("organisation", jUser)
            Me.Function = SafeGetJsonData("function", jUser)
            Me.Phone = SafeGetJsonData("phone", jUser)
            Me.Mobile = SafeGetJsonData("mobile", jUser)
            Me.Date_of_birth = SafeGetJsonData("date_of_birth", jUser)
            Me.Place_of_birth = SafeGetJsonData("place_of_birth", jUser)
            Me.Lastname_prefix = SafeGetJsonData("lastname_prefix", jUser)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function SafeGetJsonData(sProperty As String, juser As JObject) As String
        Try
            Dim jTokenVal As JToken = juser(sProperty)
            If IsNothing(jTokenVal) Then Return ""
            If Not IsDBNull(jTokenVal) Then
                Return jTokenVal.ToString

            Else
                Return ""
            End If



        Catch ex As Exception
            Return ""
        End Try

    End Function

End Class
