using System;
using System.Collections.Generic;
using System.Linq;
using FinGameWorks.Scripts.Datas.Singletons;
using UniRx.Async;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using FontStyle = Unity.UIWidgets.ui.FontStyle;

namespace FinGameWorks.Scripts.Views
{
    public class SettingsSubPanel
    {
        public bool expanded;
        public Func<Widget> header;
        public Func<Widget> body;
    }

    public class NodeEditorSettingsWidget : StatefulWidget
    {
        public override State createState()
        {
            return new NodeEditorSettingsWidgetState();
        }
    }

    public class NodeEditorSettingsWidgetState : State<NodeEditorSettingsWidget>
    {
        public override void initState()
        {
            base.initState();
            SettingsSubPanel interfacePanel = new SettingsSubPanel
            {
                header = () => new ListTile(title:new Text("Interface",style:new TextStyle( fontSize:28,fontWeight:FontWeight.w100,fontStyle:FontStyle.italic))),
                body = () => new Column
                (
                    children: new List<Widget>
                    {
                        new ListTile
                        (
                            title: new Text("FPS Counter"),
                            trailing: new Switch
                            (
                                value: NodeEditorSettingsManager.Instance.frameCounterEnabled.Value,
                                onChanged: value =>
                                {
                                    setState(() =>
                                    {
                                        if (value != null)
                                        {
                                            NodeEditorSettingsManager.Instance.frameCounterEnabled.Value = value.Value;
                                        }
                                    });
                                }
                            )
                        )
                    }
                ),
                expanded = false
            };
            dataSource.Add(interfacePanel);
        }

        private readonly List<SettingsSubPanel> dataSource = new List<SettingsSubPanel>();
        
        
        public override Widget build(BuildContext context)
        {
            return new Scaffold
            (
                appBar:new AppBar
                (
                    title:new Text("Settings")
                ),
                body:new Container
                (
                    child:new SingleChildScrollView
                    (
                        child:new ExpansionPanelList
                        (
                            children: dataSource.Select(panel =>
                                {
                                    return new ExpansionPanel
                                    (
                                        (buildContext, expanded) => panel.header.Invoke(),
                                        panel.body.Invoke(),
                                        panel.expanded
                                    );
                                }).ToList(),
                            expansionCallback:(index, expanded) =>
                            {
                                setState(() =>
                                {
                                    dataSource[index].expanded = !dataSource[index].expanded;
                                });
                            },
                            animationDuration: TimeSpan.FromSeconds(0.2)
                        )
                    )
                )
            );
        }
    }
}