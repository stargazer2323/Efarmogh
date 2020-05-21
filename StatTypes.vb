Public Class StatTypes
    'mean


    Public Function meanType(ByVal scores() As Integer)
        Dim sum As Double

        For x = 0 To scores.Length - 1
            sum += scores(x)

        Next
        Dim mean As Double = sum / scores.Length



        Return mean
    End Function




    Public Function varianceType(ByVal scores() As Integer, ByVal mean As Double)
        Dim variance As Double
        For x = 0 To scores.Length - 1
            variance += (scores(x) - mean) ^ 2
        Next
        Dim newVariance = variance / scores.Length

        Return newVariance
    End Function




    Public Function standarDeviationType(ByVal variance As Double)

        Dim standarDeviation As Double = Math.Sqrt(variance)

        Return standarDeviation


    End Function




    Public Function kurtosisType(ByVal scores() As Integer, ByVal mean As Double, ByVal standarDeviation As Double)
        Dim athroismaArithmith As Double
        Dim kurtosis As Double

        For x = 0 To scores.Length - 1
            athroismaArithmith += (scores(x) - mean) ^ 4 / scores.Length

            kurtosis = athroismaArithmith / (standarDeviation ^ 4)
        Next

        Return kurtosis

    End Function




    Public Function skewnessType(ByVal scores() As Integer, ByVal mean As Double, ByVal standarDeviation As Double)
        Dim athroismaArithmith As Double
        Dim skewness As Double

        For x = 0 To scores.Length - 2
            athroismaArithmith += (scores(x) - mean) ^ 3 / scores.Length

            skewness = athroismaArithmith / (standarDeviation ^ 3)
        Next

        Return skewness

    End Function


    Public Function criticalRatioType(ByVal scores() As Integer, ByVal mean As Double, ByVal standared As Double)
        Dim summ As Double
        Dim cr As Double
        For k = 0 To scores.Length - 2
            summ += scores(k) - mean
            cr = summ / standared
        Next


        Return cr

    End Function



    Public Function correlationType(ByVal scores1() As Integer, ByVal scores2() As Integer, ByVal mean1 As Double, ByVal mean2 As Double)
        Dim sum1 As Double
        Dim sum2 As Double
        Dim multiSum As Double
        Dim squareSum1 As Double
        Dim squareSum2 As Double

        For x = 0 To scores1.Length - 1
            sum1 += scores1(x)
            sum2 += scores2(x)
            squareSum1 += scores1(x) ^ 2
            squareSum2 += scores2(x) ^ 2
            multiSum += scores1(x) * scores2(x)

        Next

        Dim numerator As Double = scores1.Length * multiSum - sum1 * sum2
        Dim denominator As Double = Math.Sqrt(scores1.Length * squareSum1 - sum1 ^ 2) * Math.Sqrt(scores1.Length * squareSum2 - sum2 ^ 2)
        Dim correlation As Double = numerator / denominator

        Return correlation
    End Function


    'sxesh meta3y meblhtvn 

    Public Function findingCICorrelation(ByVal standarDeviation1 As Double, ByVal standarDeviation2 As Double, ByVal correlation As Double, ByVal mean1 As Double, ByVal mean2 As Double, ByVal scores1() As Integer, ByVal scores2() As Integer)
        Dim b1 As Double
        Dim b0 As Double
        Dim residuals As Double
        Dim standarDeviationEstimator As Double
        Dim sampleStandarDeviationB1 As Double
        Dim sampleStandarDeviationB0 As Double
        Dim sum1 As Double
        Dim sum2 As Double
        Dim minciB1 As Double
        Dim maxciB1 As Double
        Dim minciB0 As Double
        Dim maxciB0 As Double
        Dim t As Double = 1.645
        Dim maxci As Double
        Dim minci As Double



        b1 = standarDeviation1 * correlation / standarDeviation2
        b0 = mean2 - mean1 * b1

        For x = 0 To scores1.Length - 1

            sum1 += (scores1(x) - mean1) ^ 2
            residuals += (scores2(x) - b1 * scores1(x) - b0) ^ 2

        Next


        standarDeviationEstimator = Math.Sqrt(residuals / (scores1.Length - 2))

        sampleStandarDeviationB1 = standarDeviationEstimator / Math.Sqrt(sum1)
        sampleStandarDeviationB0 = standarDeviationEstimator * Math.Sqrt((1 / scores1.Length) + ((mean1 ^ 2) / sum1))


        minciB0 = b0 - t * sampleStandarDeviationB0
        maxciB0 = b0 + t * sampleStandarDeviationB0
        minciB1 = b1 + t * sampleStandarDeviationB1
        maxciB1 = b1 + t * sampleStandarDeviationB1


        maxci = maxciB0 + maxciB1
        minci = minciB0 + minciB1

        Dim CICorrelation = (maxci + minci) / (2 * 100)
        Return CICorrelation

    End Function




    Public Function confidenceIntervalType(ByVal mean As Double, ByVal standarDeviation As Double, ByVal scores() As Integer)


        Dim t As Double = 1.645
        Dim confidenceIntervalMin As Double
        Dim confidenceIntervalMax As Double


        confidenceIntervalMin = mean - t * standarDeviation / Math.Sqrt(scores.Length)
        confidenceIntervalMin = mean + t * standarDeviation / Math.Sqrt(scores.Length)


        Return (confidenceIntervalMin + confidenceIntervalMax) / (2 * 100)

    End Function



End Class