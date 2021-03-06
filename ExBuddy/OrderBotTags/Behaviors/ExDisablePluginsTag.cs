﻿#pragma warning disable 1998

namespace ExBuddy.OrderBotTags.Behaviors
{
    using Clio.XmlEngine;
    using ExBuddy.Attributes;
    using ff14bot.Managers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Media;

    [LoggerName("ExDisablePlugins")]
    [XmlElement("ExDisablePlugins")]
    public sealed class ExDisablePluginsTag : ExProfileBehavior
    {
        private IList<string> namesList;

        [XmlAttribute("Names")]
        public string Names { get; set; }

        protected override Color Info
        {
            get { return Colors.PaleGoldenrod; }
        }

        private IList<string> NamesList
        {
            get
            {
                if (Names == null)
                {
                    return new string[] { };
                }

                return namesList
                       ??
                       (namesList = Names.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray());
            }
        }

        protected override async Task<bool> Main()
        {
            return true;
        }

        protected override void OnStart()
        {
            if (NamesList == null || NamesList.Count == 0)
            {
                isDone = true;
                return;
            }

            StatusText = Localization.Localization.ExDisablePlugins_Disabling + Names;
            Logger.Info(Localization.Localization.ExDisablePlugins_Disabling + Names);

            foreach (var plugin in
                PluginManager.Plugins.Where(p => NamesList.Contains(p.Plugin.Name, StringComparer.InvariantCultureIgnoreCase)))
            {
                try
                {
                    if (!plugin.Enabled)
                    {
                        Logger.Info(Localization.Localization.ExDisablePlugins_Disabled, plugin.Plugin.Name);
                    }
                    else
                    {
                        Logger.Info(Localization.Localization.ExDisablePlugins_Disabling2, plugin.Plugin.Name);
                        plugin.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                }
            }

            isDone = true;
        }
    }
}