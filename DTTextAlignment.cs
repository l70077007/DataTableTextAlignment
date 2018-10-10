using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{

    /// <summary>
    ///文本对齐
    /// </summary>
    public class DTTextAlignment : IDisposable
    {
        private DataTable Dt;

        Graphics graphics = null;

        Font font;

        /// <summary>
        /// 使用空格分隔
        /// </summary>
        char space = ' ';


        /// <summary>
        /// 数据的开始符号
        /// </summary>
        public string StartFlag = string.Empty;

        /// <summary>
        /// 数据的结束符号
        /// </summary>
        public string EndFlag = string.Empty;

        /// <summary>
        /// 列间距多少个空格
        /// </summary>
        public int ColSpaceCount = 5;

        /// <summary>
        /// 是否所有的列一样长，还是按照当前列取最大长
        /// </summary>
        public bool IsAllColSinLengh;

        float spaceWith;

        public DTTextAlignment(DataTable _Dt, Graphics G, Font _font)
        {
            graphics = G;
            font = _font;
            this.Dt = _Dt;
            //两个空格长度减去一个空格长度，才是一个空格占用的真正长度
            spaceWith = GetLe(new string(space, 20) + '-') - GetLe(new string(space, 19) + '-');

        }

        private float GetMaxColWidth()
        {
            float result = 0;

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                for (int c = 0; c < Dt.Columns.Count; c++)
                {
                    string data = StartFlag + Dt.Rows[i][c].ToString() + EndFlag;
                    float f = GetLe(data);
                    result = result > f ? result : f;
                }

            }
            return result;
        }
        public string GetResultStr()
        {
            return String.Join("\n", GetResultArray());
        }


        /// <summary>
        /// 生成结果
        /// </summary>
        /// <returns></returns>
        public string[] GetResultArray()
        {
            float MaxFloat = GetMaxColWidth()+ ColSpaceCount;

            List<string> resultList = new List<string>();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                StringBuilder sbLine = new StringBuilder();
                for (int c = 0; c < Dt.Columns.Count; c++)
                {
                    StringBuilder sb = new StringBuilder();
                    //当前Cell
                    string data = StartFlag + Dt.Rows[i][c].ToString() + EndFlag;
                    //计算应该使用多少个空格来填充
                    int spaceCount = (int)((MaxFloat - GetLe(data)) / spaceWith);

                    //对齐方式
                    switch (TextPad)
                    {
                        case TextPadEnum.Left:
                            sb.Append(data + new string(space, spaceCount));
                            break;
                        case TextPadEnum.Right:
                            sb.Append(new string(space, spaceCount) + data);
                            break;
                        case TextPadEnum.Middle:

                            int every = (int)(spaceCount / 2);
                            sb.Append(new string(space, every) + data + new string(space, every));
                            if (spaceCount %2 == 1)
                            {
                                sb.Append(space);
                            }
                            break;
                        default:
                            break;
                    }
                    sbLine.Append(sb);
                }
                resultList.Add(sbLine.ToString());
            }

            return resultList.ToArray();

        }





        /// <summary>
        /// 对齐方式
        /// </summary>
        public TextPadEnum TextPad = TextPadEnum.Left;


        public void Dispose()
        {

            try
            {
                graphics.Dispose();
            }
            catch (Exception)
            {


            }
        }

        private float GetLe(string text)
        {

            SizeF sizeF = graphics.MeasureString(text, font);

            return sizeF.Width;

        }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public enum TextPadEnum
        {
            /// <summary>
            /// 左对齐
            /// </summary>
            Left,

            /// <summary>
            /// 又对齐
            /// </summary>
            Right,

            /// <summary>
            /// 中间对齐
            /// </summary>
            Middle
        }

    }
}
