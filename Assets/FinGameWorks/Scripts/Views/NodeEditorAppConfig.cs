using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using FontStyle = Unity.UIWidgets.ui.FontStyle;

namespace FinGameWorks.Scripts.Views
{
    public static class NodeEditorAppConfig
    {
        public static void LoadFonts()
        {
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/Material/MaterialIcons-Regular"), "Material Icons");
            
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/IBM-Plex-Mono/IBMPlexMono-ExtraLight"), "IBM Plex Mono", FontWeight.w100,FontStyle.normal);
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/IBM-Plex-Mono/IBMPlexMono-ExtraLightItalic"), "IBM Plex Mono", FontWeight.w100,FontStyle.italic);
            
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/IBM-Plex-Mono/IBMPlexMono-Regular"), "IBM Plex Mono", FontWeight.normal,FontStyle.normal);
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/IBM-Plex-Mono/IBMPlexMono-Italic"), "IBM Plex Mono", FontWeight.normal,FontStyle.italic);
            
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/IBM-Plex-Mono/IBMPlexMono-Bold"), "IBM Plex Mono", FontWeight.bold,FontStyle.normal);
            FontManager.instance.addFont(Resources.Load<Font>("Fonts/IBM-Plex-Mono/IBMPlexMono-BoldItalic"), "IBM Plex Mono", FontWeight.bold,FontStyle.italic);
        }

        public static ThemeData DayTheme()
        {
            return new ThemeData(
                fontFamily: "IBM Plex Mono",
                primarySwatch: Colors.blueGrey,
                indicatorColor: Colors.blueGrey.shade200,
                scaffoldBackgroundColor: Colors.grey.shade200,
                backgroundColor: Colors.grey.shade200
            );
        }
        
        public static ThemeData NightTheme()
        {
            return new ThemeData(
                fontFamily: "IBM Plex Mono",
                primarySwatch: Colors.blueGrey,
                indicatorColor: Colors.blueGrey.shade200,
                scaffoldBackgroundColor: Colors.grey.shade200,
                backgroundColor: Colors.grey.shade200
            );
        }
    }
}