Public Class cQueueItem
    Private _Tabel As String
    Private _StudentNr As Decimal
    Private _datumTijd As Date

    Public Property Tabel As String
        Get
            Return _Tabel
        End Get
        Set(value As String)
            _Tabel = value
        End Set
    End Property

    Public Property StudentNr As Decimal
        Get
            Return _StudentNr
        End Get
        Set(value As Decimal)
            _StudentNr = value
        End Set
    End Property

    Public Property DatumTijd As Date
        Get
            Return _datumTijd
        End Get
        Set(value As Date)
            _datumTijd = value
        End Set
    End Property
    Public Sub New()

    End Sub

    Public Sub New(tabel As String, studentNr As Decimal, datumTijd As Date)
        If tabel Is Nothing Then
            Throw New ArgumentNullException(NameOf(tabel))
        End If


        Me.Tabel = tabel
        Me.StudentNr = studentNr
        Me.DatumTijd = datumTijd
    End Sub
End Class
