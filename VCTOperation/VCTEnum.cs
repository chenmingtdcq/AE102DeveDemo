using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation
{
    public enum VCTEnum
    {
        HeadBegin,
        HeadEnd,


    }

    public enum VCTGeometryType
    {
        Point,
        Line,
        Polygon,
        Solid,
        Annotation
    }

    public enum VCTFieldType
    {
        Char,
        Int1,
        Int2,
        Int4,
        Int8,
        Float,
        Double,
        Date,
        Time,
        DateTime,
        Varchar,
        Varbin
    }

    public enum VCTPointType
    {
        DLD = 1,
        JD = 2,
        YXD = 3,
        DC = 4,
    }

    public enum VCTPolylineType
    {
        ZJZBX = 1,
        JJZBX = 100,
    }

    public enum VCTSegmentType
    {
        ZX = 11,
        SDYH = 12,
        YXH = 13,
        TYH = 14,
        SCYTQX = 15,
        BYQX = 16,
        BSEQX = 17,
    }

    public enum VCTPolygonType
    {
        ZJZBM = 1,
        JJZBM = 100,
    }

    public enum VCTPolygonGeometryType
    {
        SimplePolygon = 11,
        ThreePointCircle = 12,
        CircleCenterCircle = 13,
        Ellipse = 14,
    }

    public enum VCT3DType
    {
        ZJZBT = 1,
        JJZBT = 100,
    }

    public enum VCT3DGeometryType
    {
        Tetrahedron = 11,
        Cuboid = 12,
        Rotator = 19,
    }

    public enum VCTAnnotationType
    {
        SingleAnnotation = 1,
        MultiAnnotation = 2,
    }

    public class VCTConst
    {
        public static string CR = "\r\n";
        public static string Colon = ":";
    }
}
