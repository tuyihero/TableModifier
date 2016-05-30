using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Collada4Res
{
    public class WriteCollada
    {
        #region 唯一

        private static WriteCollada _Instance;
        public static WriteCollada Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new WriteCollada();
                }
                return _Instance;
            }
        }

        private WriteCollada() { }

        #endregion

        #region 
        //新建XML信息
        public const string INIT_VERSION_STR = "1.0";
        public const string INIT_ENCODING_STR = "utf-8";
        public const string DOC_ROOT_STR = "COLLADA";

        public const string COLLADA_DIRECT_PATH = "./Collada";

        private string _FileName = "";
        #endregion

        #region write
        public float[] _TestPos = new float[24] { 1, 1, -1, 1, -1, -1, -1, -0.9999998f, -1, -0.9999997f, 1, -1, 1, 0.9999995f, 1, 1, -1.000001f, 1, -1, -0.9999997f, 1, -1, 1, 1 };
        public float[] _TestNormal = new float[36] { 0, 0, -1, 0, 0, 1, 1, 0, -2.38419e-7f, 0, -1, -4.76837e-7f, -1, 2.38419e-7f, -1.49012e-7f, 2.68221e-7f, 1, 2.38419e-7f, 0, 0, -1, 0, 0, 1, 1, -5.96046e-7f, 3.27825e-7f, -4.76837e-7f, -1, 0, -1, 2.38419e-7f, -1.19209e-7f, 2.08616e-7f, 1, 0 };
        public int[] _TestPoly = new int[72] { 0, 0, 2, 0, 3, 0, 7, 1, 5, 1, 4, 1, 4, 2, 1, 2, 0, 2, 5, 3, 2, 3, 1, 3, 2, 4, 7, 4, 3, 4, 0, 5, 7, 5, 4, 5, 0, 6, 1, 6, 2, 6, 7, 7, 6, 7, 5, 7, 4, 8, 5, 8, 1, 8, 5, 9, 6, 9, 2, 9, 2, 10, 6, 10, 7, 10, 0, 11, 3, 11, 7, 11 };
        public int _TestPolyCount = 12;
        public float[] _TestMatrix = { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };

        public void WriteFile(string directPath, string fileName, float[] pos, float[] normal, int[] poly, int polyCount, float[] matrix)
        {
            //string directPath = COLLADA_DIRECT_PATH;
            string filePath = directPath + "/" + fileName + ".dae";
            _FileName = fileName;
            if (!Directory.Exists(directPath))
            {
                Directory.CreateDirectory(directPath);
            }

            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(filePath))
            {
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(INIT_VERSION_STR, INIT_ENCODING_STR, null);
                xmlDoc.AppendChild(dec);
                XmlElement rootInit = xmlDoc.CreateElement(DOC_ROOT_STR);
                rootInit.SetAttribute("xmlns", "http://www.collada.org/2005/11/COLLADASchema");
                rootInit.SetAttribute("version", "1.5");
                xmlDoc.AppendChild(rootInit);
                xmlDoc.Save(filePath);
            }
            else
            {
                xmlDoc.Load(filePath);
            }

            XmlNode root = xmlDoc.SelectSingleNode(DOC_ROOT_STR);
            root.RemoveAll();

            WriteAsset(root);
            WriteGeometrie(root, pos, normal, polyCount, poly);
            WriteLibSceneNode(root, matrix);

            XmlElement scene = root.OwnerDocument.CreateElement("scene");
            root.AppendChild(scene);

            XmlElement instanceScene = root.OwnerDocument.CreateElement("instance_visual_scene");
            instanceScene.SetAttribute("url", "#Scene");
            scene.AppendChild(instanceScene);

            xmlDoc.Save(filePath);
        }

        private void WriteAsset(XmlNode root)
        {
            XmlElement nodeAsset = root.OwnerDocument.CreateElement("asset");

            XmlElement unit = root.OwnerDocument.CreateElement("unit");
            unit.SetAttribute("name", "meter");
            unit.SetAttribute("meter", "1");
            nodeAsset.AppendChild(unit);

            XmlElement upAxis = root.OwnerDocument.CreateElement("up_axis");
            upAxis.InnerText = "Z_UP";
            nodeAsset.AppendChild(upAxis);

            root.AppendChild(nodeAsset);
        }

        private void WriteGeometrie(XmlNode root, float[] vertices, float[] normals, int polyCount, int[] vertexes)
        {
            XmlElement libGeometry = root.OwnerDocument.CreateElement("library_geometries");
            root.AppendChild(libGeometry);

            XmlElement geometry = root.OwnerDocument.CreateElement("geometry");
            geometry.SetAttribute("id", _FileName + "-mesh");
            geometry.SetAttribute("name", _FileName);
            libGeometry.AppendChild(geometry);

            XmlElement mesh = root.OwnerDocument.CreateElement("mesh");
            geometry.AppendChild(mesh);

            WritePositionNode(mesh, vertices);
            WriteNormalNode(mesh, normals);
            WriteVerticesNode(mesh);
            WritePolyNode(mesh, polyCount, vertexes);
        }

        private void WritePositionNode(XmlElement root, float[] vertices)
        {
            string valueStr = "";
            int verticeLength = vertices.Length;
            for (int i = 0; i < verticeLength; ++i)
            {
                valueStr += vertices[i].ToString() + " ";
            }

            XmlElement source1 = root.OwnerDocument.CreateElement("source");
            source1.SetAttribute("id", _FileName + "-mesh-positions");

            XmlElement floatArrary = root.OwnerDocument.CreateElement("float_array");
            floatArrary.SetAttribute("id", _FileName + "-mesh-positions-array");
            floatArrary.SetAttribute("count", verticeLength.ToString());
            floatArrary.InnerText = valueStr;
            source1.AppendChild(floatArrary);

            XmlElement techCommon = root.OwnerDocument.CreateElement("technique_common");
            source1.AppendChild(techCommon);

            XmlElement accessor = root.OwnerDocument.CreateElement("accessor");
            accessor.SetAttribute("source", "#" + _FileName + "-mesh-positions-array");
            accessor.SetAttribute("count", (verticeLength / 3).ToString());
            accessor.SetAttribute("stride", "3");
            techCommon.AppendChild(accessor);

            XmlElement param1 = root.OwnerDocument.CreateElement("param");
            param1.SetAttribute("name", "X");
            param1.SetAttribute("type", "float");
            accessor.AppendChild(param1);

            XmlElement param2 = root.OwnerDocument.CreateElement("param");
            param2.SetAttribute("name", "Y");
            param2.SetAttribute("type", "float");
            accessor.AppendChild(param2);

            XmlElement param3 = root.OwnerDocument.CreateElement("param");
            param3.SetAttribute("name", "Z");
            param3.SetAttribute("type", "float");
            accessor.AppendChild(param3);

            root.AppendChild(source1);
        }

        private void WriteNormalNode(XmlElement root, float[] normals)
        {
            string valueStr = "";
            int normalLength = normals.Length;
            for (int i = 0; i < normalLength; ++i)
            {
                valueStr += normals[i].ToString() + " ";
            }

            XmlElement source1 = root.OwnerDocument.CreateElement("source");
            source1.SetAttribute("id", _FileName + "-mesh-normals");

            XmlElement floatArrary = root.OwnerDocument.CreateElement("float_array");
            floatArrary.SetAttribute("id", _FileName + "-mesh-normals-array");
            floatArrary.SetAttribute("count", normalLength.ToString());
            floatArrary.InnerText = valueStr;
            source1.AppendChild(floatArrary);

            XmlElement techCommon = root.OwnerDocument.CreateElement("technique_common");
            source1.AppendChild(techCommon);

            XmlElement accessor = root.OwnerDocument.CreateElement("accessor");
            accessor.SetAttribute("source", "#" + _FileName + "-mesh-normals-array");
            accessor.SetAttribute("count", (normalLength / 3).ToString());
            accessor.SetAttribute("stride", "3");
            techCommon.AppendChild(accessor);

            XmlElement param1 = root.OwnerDocument.CreateElement("param");
            param1.SetAttribute("name", "X");
            param1.SetAttribute("type", "float");
            accessor.AppendChild(param1);

            XmlElement param2 = root.OwnerDocument.CreateElement("param");
            param2.SetAttribute("name", "Y");
            param2.SetAttribute("type", "float");
            accessor.AppendChild(param2);

            XmlElement param3 = root.OwnerDocument.CreateElement("param");
            param3.SetAttribute("name", "Z");
            param3.SetAttribute("type", "float");
            accessor.AppendChild(param3);

            root.AppendChild(source1);
        }

        private void WriteVerticesNode(XmlElement root)
        {

            XmlElement vertices = root.OwnerDocument.CreateElement("vertices");
            vertices.SetAttribute("id", _FileName + "-mesh-vertices");

            XmlElement input = root.OwnerDocument.CreateElement("input");
            input.SetAttribute("semantic", "POSITION");
            input.SetAttribute("source", "#" + _FileName + "-mesh-positions");
            vertices.AppendChild(input);

            root.AppendChild(vertices);
        }

        private void WritePolyNode(XmlElement root, int polyCount, int[] vertexes)
        {
            int vertexLength = vertexes.Length;
            string vertexStr = "";
            string countStr = "";
            for (int i = 0; i < vertexLength; ++i)
            {
                vertexStr += vertexes[i].ToString() + " ";
            }

            for (int i = 0; i < polyCount; ++i)
            {
                countStr += "3 ";
            }

            XmlElement poly = root.OwnerDocument.CreateElement("triangles");
            poly.SetAttribute("material", "Material-material");
            poly.SetAttribute("count", polyCount.ToString());

            XmlElement input = root.OwnerDocument.CreateElement("input");
            input.SetAttribute("semantic", "VERTEX");
            input.SetAttribute("source", "#" + _FileName + "-mesh-vertices");
            input.SetAttribute("offset", "0");
            poly.AppendChild(input);

            XmlElement input2 = root.OwnerDocument.CreateElement("input");
            input2.SetAttribute("semantic", "NORMAL");
            input2.SetAttribute("source", "#" + _FileName + "-mesh-normals");
            input2.SetAttribute("offset", "1");
            poly.AppendChild(input2);

            XmlElement p = root.OwnerDocument.CreateElement("p");
            p.InnerText = vertexStr;
            poly.AppendChild(p);

            root.AppendChild(poly);
        }

        private void WriteLibSceneNode(XmlNode root, float[] tranMatrix)
        {
            int matrixLength = tranMatrix.Length;
            string matrixStr = "";
            for (int i = 0; i < matrixLength; ++i)
            {
                matrixStr += tranMatrix[i].ToString() + " ";
            }

            XmlElement libScene = root.OwnerDocument.CreateElement("library_visual_scenes");

            XmlElement visualScene = root.OwnerDocument.CreateElement("visual_scene");
            visualScene.SetAttribute("id", "Scene");
            visualScene.SetAttribute("name", "Scene");
            libScene.AppendChild(visualScene);

            XmlElement node = root.OwnerDocument.CreateElement("node");
            node.SetAttribute("id", _FileName);
            node.SetAttribute("name", _FileName);
            node.SetAttribute("type", "NODE");
            visualScene.AppendChild(node);

            XmlElement matrix = root.OwnerDocument.CreateElement("matrix");
            matrix.SetAttribute("sid", "transform");
            matrix.InnerText = matrixStr;
            node.AppendChild(matrix);

            XmlElement instanceGeo = root.OwnerDocument.CreateElement("instance_geometry");
            instanceGeo.SetAttribute("url", "#" + _FileName + "-mesh");
            instanceGeo.SetAttribute("name", _FileName);
            node.AppendChild(instanceGeo);

            XmlElement bindMaterial = root.OwnerDocument.CreateElement("bind_material");
            instanceGeo.AppendChild(bindMaterial);

            XmlElement techCommon = root.OwnerDocument.CreateElement("technique_common");
            bindMaterial.AppendChild(techCommon);

            XmlElement instanceMaterial = root.OwnerDocument.CreateElement("instance_material");
            instanceMaterial.SetAttribute("symbol", "Material-material");
            instanceMaterial.SetAttribute("target", "#Material-material");
            techCommon.AppendChild(instanceMaterial);

            root.AppendChild(libScene);
        }
        #endregion
    }
}
