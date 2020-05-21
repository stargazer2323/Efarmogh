Imports Efarmogh

Public Class ClassType

    Implements IComparable(Of ClassType)

    Public num As Integer = 1
    Public sum As Integer
    Public name As Integer
    Public allvalues As List(Of Integer) = New List(Of Integer)


    Public Function SortReq(other As ClassType) As Integer Implements IComparable(Of ClassType).CompareTo
        If (Me.name = other.name) Then
            Return 0
        ElseIf (Me.name > other.name) Then
            Return 1
        Else
            Return -1
        End If
    End Function


    Public Sub New(name As Integer, sum As Integer)

        Me.name = name
        Me.sum = sum
        allvalues.Add(sum)

    End Sub

    Friend Function getMeanType() As Double
        Dim meantype As Double = sum / num
        Return meantype
    End Function




    Friend Function varianceType() As Double
        Dim variance As Double
        Dim mean As Double = getMeanType()
        For x = 0 To allvalues.Count - 1
            variance += (allvalues(x) - mean) ^ 2
        Next
        Dim newVariance = variance / allvalues.Count

        Return newVariance
    End Function

    Friend Function standarDeviationType() As Double

        Dim standarDeviation As Double = Math.Sqrt(varianceType())
        Return standarDeviation

    End Function

End Class
