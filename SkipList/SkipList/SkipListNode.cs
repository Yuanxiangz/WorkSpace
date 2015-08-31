/*
 * author : qiang.xu
 * 
 * blog : http://www.cnblogs.com/xuqiang
 * 
 * this is a free algorithm library just for learning, and you
 * and modify, share the source code as you wish.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyCoding.Collections
{
    /// <summary>
    /// 跳表节点类型
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SkipListNode<TKey, TValue>
    {
        #region 数据成员

        private TKey thisKey;
        private TValue thisValue;
        private SkipListNode<TKey, TValue> rightNode, downNode;

        #endregion

        #region 构造函数

        internal SkipListNode() { }

        internal SkipListNode(TKey key, TValue val)
        {
            thisKey = key;
            thisValue = val;
        }

        #endregion

        #region 属性

        public TKey Key
        {
            get
            {
                return thisKey;
            }
            set
            {
                thisKey = value;
            }
        }

        public TValue Value
        {
            get
            {
                return thisValue;
            }
            set
            {
                thisValue = value;
            }
        }

        /// <summary>
        /// 右节点
        /// </summary>
        public SkipListNode<TKey, TValue> Right
        {
            get
            {
                return rightNode;
            }
            set
            {
                rightNode = value;
            }
        }

        /// <summary>
        /// 下面的节点
        /// </summary>
        public SkipListNode<TKey, TValue> Down
        {
            get
            {
                return downNode;
            }
            set
            {
                downNode = value;
            }
        }

        #endregion

    }
}
