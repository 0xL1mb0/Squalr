﻿using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Anathema
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class ScriptTable : IScriptTableModel
    {
        private static ScriptTable ScriptTableInstance;

        private List<ScriptItem> ScriptItems;

        public event ScriptTableEventHandler EventClearScriptCacheItem;
        public event ScriptTableEventHandler EventClearScriptCache;

        private ScriptTable()
        {
            ScriptItems = new List<ScriptItem>();
        }

        public static ScriptTable GetInstance()
        {
            if (ScriptTableInstance == null)
                ScriptTableInstance = new ScriptTable();
            return ScriptTableInstance;
        }

        private void RefreshDisplay()
        {
            // Request that all data be updated
            ScriptTableEventArgs Args = new ScriptTableEventArgs();
            Args.ItemCount = ScriptItems.Count;
            EventClearScriptCache(this, Args);
        }
        
        public void OpenScript(Int32 Index)
        {
            if (Index >= ScriptItems.Count)
                return;

            Main.GetInstance().OpenScriptEditor();
            ScriptEditor.GetInstance().OpenScript(ScriptItems[Index]);
        }

        public void SaveScript(ScriptItem ScriptItem)
        {
            if (!ScriptItems.Contains(ScriptItem))
            {
                // Adding a new script
                ScriptItems.Add(ScriptItem);

                ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
                ScriptTableEventArgs.ItemCount = ScriptItems.Count;
                EventClearScriptCache(this, ScriptTableEventArgs);
            }
            else
            {
                // Updating an existing script, clear it from the cache
                ClearScriptItemFromCache(ScriptItem);
            }
        }

        public ScriptItem GetScriptItemAt(Int32 Index)
        {
            return ScriptItems[Index];
        }

        public void SetScriptActivation(Int32 Index, Boolean Activated)
        {
            // Try to update the activation state
            ScriptItems[Index].SetActivationState(Activated);
            ClearScriptItemFromCache(ScriptItems[Index]);
        }

        private void ClearScriptItemFromCache(ScriptItem ScriptItem)
        {
            ScriptTableEventArgs ScriptTableEventArgs = new ScriptTableEventArgs();
            ScriptTableEventArgs.ClearCacheIndex = ScriptItems.IndexOf(ScriptItem);
            ScriptTableEventArgs.ItemCount = ScriptItems.Count;
            EventClearScriptCacheItem(this, ScriptTableEventArgs);
        }

    } // End class

} // End namespace