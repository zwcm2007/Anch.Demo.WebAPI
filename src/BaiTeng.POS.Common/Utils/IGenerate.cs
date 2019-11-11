using System;

namespace GisqRealEstate.MaintainWeb.Common
{
    public interface IGenerate
    {
        string GreateNewYWH();

        Int64 GenerateBdcdjSeqBsm();

        Int64 GenerateSjwhSeqBsm();

        string CreateBBH();

    }
}