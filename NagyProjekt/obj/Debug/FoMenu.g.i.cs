﻿#pragma checksum "..\..\FoMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "97D8BC4A52B5485D311AF4F42912E2C5FF3A908B1D1157C4774B450E984FB208"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NagyProjekt;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace NagyProjekt {
    
    
    /// <summary>
    /// FoMenu
    /// </summary>
    public partial class FoMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\FoMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_NewGame;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\FoMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_Continue;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\FoMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_Settings;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\FoMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_About;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\FoMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image_Quit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NagyProjekt;component/fomenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FoMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Image_NewGame = ((System.Windows.Controls.Image)(target));
            
            #line 11 "..\..\FoMenu.xaml"
            this.Image_NewGame.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 11 "..\..\FoMenu.xaml"
            this.Image_NewGame.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            
            #line 11 "..\..\FoMenu.xaml"
            this.Image_NewGame.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.NewGameButton);
            
            #line default
            #line hidden
            
            #line 11 "..\..\FoMenu.xaml"
            this.Image_NewGame.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ButtonsMouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Image_Continue = ((System.Windows.Controls.Image)(target));
            
            #line 12 "..\..\FoMenu.xaml"
            this.Image_Continue.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\FoMenu.xaml"
            this.Image_Continue.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            
            #line 12 "..\..\FoMenu.xaml"
            this.Image_Continue.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ContinueButton);
            
            #line default
            #line hidden
            
            #line 12 "..\..\FoMenu.xaml"
            this.Image_Continue.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ButtonsMouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Image_Settings = ((System.Windows.Controls.Image)(target));
            
            #line 13 "..\..\FoMenu.xaml"
            this.Image_Settings.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 13 "..\..\FoMenu.xaml"
            this.Image_Settings.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            
            #line 13 "..\..\FoMenu.xaml"
            this.Image_Settings.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SettingsButton);
            
            #line default
            #line hidden
            
            #line 13 "..\..\FoMenu.xaml"
            this.Image_Settings.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ButtonsMouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Image_About = ((System.Windows.Controls.Image)(target));
            
            #line 14 "..\..\FoMenu.xaml"
            this.Image_About.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 14 "..\..\FoMenu.xaml"
            this.Image_About.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            
            #line 14 "..\..\FoMenu.xaml"
            this.Image_About.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AboutButton);
            
            #line default
            #line hidden
            
            #line 14 "..\..\FoMenu.xaml"
            this.Image_About.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ButtonsMouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Image_Quit = ((System.Windows.Controls.Image)(target));
            
            #line 15 "..\..\FoMenu.xaml"
            this.Image_Quit.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.QuitButton);
            
            #line default
            #line hidden
            
            #line 15 "..\..\FoMenu.xaml"
            this.Image_Quit.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\FoMenu.xaml"
            this.Image_Quit.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            
            #line 15 "..\..\FoMenu.xaml"
            this.Image_Quit.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ButtonsMouseUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

