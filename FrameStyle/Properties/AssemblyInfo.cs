﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using System.Resources;
using System;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("FrameStyle")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("FrameStyle")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("d2ef55c3-d93a-49d4-a28f-14649cd4ff11")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      生成号
//      修订号
//
// 可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: XmlnsDefinition("http://tablemodify.com/FramStyle", "UITemplate.Controls")]
[assembly: XmlnsPrefix("http://tablemodify.com/FramStyle", "tfs")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //主题特定资源词典所处位置
    //(在页面或应用程序资源词典中 
    // 未找到某个资源的情况下使用)
    ResourceDictionaryLocation.SourceAssembly //常规资源词典所处位置
    //(在页面、应用程序或任何主题特定资源词典中
    // 未找到某个资源的情况下使用)
)]