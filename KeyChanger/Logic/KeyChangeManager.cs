using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyChanger
{
    public class KeyChangeManager
    {
        #region 静态唯一

        private static KeyChangeManager _Instance = null;
        public static KeyChangeManager Instance 
        {
            get
            {
                if(_Instance == null)
                    _Instance = new KeyChangeManager();
                return _Instance;
            }
        }

        private KeyChangeManager()
        {
            KeyChangeItemCollection = new KeyChangeItemCollection();
        }
        #endregion

        #region 临时变量 避免频繁创建

        #endregion

        private List<KeyChangeItem> _tRemoveKey = new List<KeyChangeItem>();

        #region

        public KeyChangeItemCollection KeyChangeItemCollection { get; set; }

        //按键匹配优先级：默认的，组合键复杂的优先级高
        private Dictionary<string, List<KeyChangeItem>> _KeyMatchPrior = new Dictionary<string,List<KeyChangeItem>>();

        //hook准备
        public void StartHookInit()
        {
            _KeyMatchPrior.Clear();
            foreach (KeyChangeItem keyItem in KeyChangeItemCollection)
            {
                foreach (KeyStoreInfo keyInfo in keyItem.FromStores)
                {
                    if (!_KeyMatchPrior.ContainsKey(keyInfo.BtnName))
                    {
                        List<KeyChangeItem> itemList = new List<KeyChangeItem>();
                        itemList.Add(keyItem);
                        _KeyMatchPrior.Add(keyInfo.BtnName, itemList);
                    }
                    else
                    {
                        _KeyMatchPrior[keyInfo.BtnName].Add(keyItem);
                        _KeyMatchPrior[keyInfo.BtnName].Sort((keyitem1, keyitem2) => 
                        {
                            if (keyitem1.FromStores.Count > keyitem2.FromStores.Count)
                                return 1;
                            else if (keyitem1.FromStores.Count < keyitem2.FromStores.Count)
                                return -1;
                            else
                                return 0;
                        });
                    }
                }
            }

            //剔除所有优先级一样的，和没有冲突的
            List<string> cullKeyPrior = new List<string>();
            foreach (KeyValuePair<string, List<KeyChangeItem>> pair in _KeyMatchPrior)
            {
                if (pair.Value.Count == 1)
                {
                    cullKeyPrior.Add(pair.Key);
                    continue;
                }

                if (pair.Value.First<KeyChangeItem>().FromStores.Count == pair.Value.Last<KeyChangeItem>().FromStores.Count)
                {
                    cullKeyPrior.Add(pair.Key);
                    continue;
                }
            }

            foreach (string cullStr in cullKeyPrior)
            {
                _KeyMatchPrior.Remove(cullStr);
            }
        }

        public KeyChangeItem NewChange()
        {
            KeyChangeItem changeItem = new KeyChangeItem();
            changeItem.FromStores.Add(new KeyStoreInfo());
            changeItem.ToStores.Add(new KeyStoreInfo());

            KeyChangeItemCollection.Add(changeItem);

            return changeItem;
        }

        public void RemoveChange(KeyChangeItem changeItem)
        {
            KeyChangeItemCollection.Remove(changeItem);
        }

        public void MatchKeyItem(JoystickButtons joyBtns, byte[] keyState, ref List<KeyChangeItem> changeKeys)
        {
            foreach (KeyChangeItem keyItem in KeyChangeItemCollection)
            {
                if (keyItem.IsDown)
                {
                    if (!keyItem.IsMatchFrom(joyBtns, keyState))
                    {
                        changeKeys.Add(keyItem);
                    }
                }
                else
                {
                    if (keyItem.IsMatchFrom(joyBtns, keyState))
                    {
                        changeKeys.Add(keyItem);
                    }
                }
            }

            if (changeKeys.Count == 0)
                return;

            _tRemoveKey.Clear();
            for (int i = 0; i < changeKeys.Count; ++i)
            {
                if (_tRemoveKey.Contains(changeKeys[i]))
                        continue;

                for (int j = i + 1; j < changeKeys.Count; ++j)
                {
                    int compare = CompareKeyPrior(changeKeys[i], changeKeys[j]);
                    if (compare > 0)
                    {
                        _tRemoveKey.Add(changeKeys[j]);
                    }
                    else if (compare < 0)
                    {
                        _tRemoveKey.Add(changeKeys[i]);
                    }
                }
            }

            foreach (KeyChangeItem removeItem in _tRemoveKey)
            {
                changeKeys.Remove(removeItem);
            }
        }

        private int CompareKeyPrior(KeyChangeItem keyItem1, KeyChangeItem keyItem2)
        {
            foreach (KeyStoreInfo keyInfo in keyItem1.FromStores)
            {
                if (_KeyMatchPrior.ContainsKey(keyInfo.BtnName))
                {
                    if (_KeyMatchPrior[keyInfo.BtnName].Contains(keyItem2))
                    {
                        if (_KeyMatchPrior[keyInfo.BtnName].IndexOf(keyItem1) > _KeyMatchPrior[keyInfo.BtnName].IndexOf(keyItem2))
                            return 1;
                        else
                            return -1;
                    }
                }
            }
            return 0;
        }

        #endregion

    }
}
