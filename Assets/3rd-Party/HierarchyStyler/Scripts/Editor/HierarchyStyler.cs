using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Hierarchy;
using Unity.Hierarchy.Editor;

[InitializeOnLoad]
public static class HierarchyStyler
{
    private const string OverlayClassName = "hierarchy-styler-overlay";
    private const string LabelName = "hierarchy-styler-label";

    private static readonly Regex Pattern =
        new(@"^(-{3,})\s*(.+)$", RegexOptions.Compiled);

    static HierarchyStyler()
    {
        HierarchyWindow.BindViewItem -= OnBindViewItem;
        HierarchyWindow.BindViewItem += OnBindViewItem;
    }

    private static void OnBindViewItem(
        HierarchyWindow window,
        HierarchyView view,
        HierarchyViewItem viewItem)
    {
        if (view == null 
            || viewItem == null)
        {
            return;
        }

        ResetViewItem(viewItem);

        EntityId entityId = view.Source.GetEntityId(viewItem.Node);

        if (entityId == EntityId.None)
        {
            return;
        }

        if (EditorUtility.EntityIdToObject(entityId) is not GameObject gameObject)
        {
            return;
        }

        Match match = Pattern.Match(gameObject.name);

        if (!match.Success)
        {
            return;
        }

        int dashCount = match.Groups[1].Value.Length;
        string title = match.Groups[2].Value;

        StyleSection(
            viewItem,
            title,
            GetColorByDashCount(dashCount));
    }

    private static void ResetViewItem(HierarchyViewItem viewItem)
    {
        var overlays = viewItem
            .Query<VisualElement>(className: OverlayClassName)
            .ToList();

        foreach (var overlay in overlays)
        {
            overlay.RemoveFromHierarchy();
        }

        var icon = viewItem.Q<VisualElement>(className: "hierarchy-item__icon");

        if (icon != null)
        {
            icon.style.display = StyleKeyword.Null;
        }

        var overlayIcon = viewItem.Q<VisualElement>(className: "hierarchy-item__overlay-icon");

        if (overlayIcon != null)
        {
            overlayIcon.style.display = StyleKeyword.Null;
        }

        var nameContainer = viewItem.Q<VisualElement>(className: "hierarchy-item__name");

        if (nameContainer != null)
        {
            nameContainer.style.display = StyleKeyword.Null;
        }
    }

    private static void StyleSection(
        HierarchyViewItem viewItem,
        string title,
        Color textColor)
    {
        var icon = viewItem.Q<VisualElement>(className: "hierarchy-item__icon");

        if (icon != null)
        {
            icon.style.display = DisplayStyle.None;
        }

        var overlayIcon = viewItem.Q<VisualElement>(className: "hierarchy-item__overlay-icon");

        if (overlayIcon != null)
        {
            overlayIcon.style.display = DisplayStyle.None;
        }

        var nameContainer = viewItem.Q<VisualElement>(className: "hierarchy-item__name");

        if (nameContainer != null)
        {
            nameContainer.style.display = DisplayStyle.None;
        }

        var overlay = new VisualElement
        {
            pickingMode = PickingMode.Ignore
        };

        overlay.AddToClassList(OverlayClassName);

        overlay.style.position = Position.Absolute;

        overlay.style.left = 0;
        overlay.style.right = 0;
        overlay.style.top = 0;
        overlay.style.bottom = 0;

        overlay.style.flexDirection = FlexDirection.Row;
        overlay.style.justifyContent = Justify.Center;
        overlay.style.alignItems = Align.Center;

        overlay.style.backgroundColor = Color.clear;

        var label = new Label(title)
        {
            name = LabelName,
            pickingMode = PickingMode.Ignore
        };

        label.style.color = textColor;
        label.style.unityFontStyleAndWeight = FontStyle.Bold;
        label.style.unityTextAlign = TextAnchor.MiddleCenter;

        label.style.flexGrow = 0;
        label.style.flexShrink = 0;

        overlay.Add(label);
        viewItem.Add(overlay);
    }

    private static Color GetColorByDashCount(int count)
    {
        return count switch
        {
            3 => new Color(0.118f, 0.471f, 0.596f),
            4 => new Color(0.561f, 0.820f, 0.620f),
            5 => new Color(1.000f, 0.550f, 0.000f),
            6 => new Color(0.600f, 1.000f, 0.400f),
            >= 7 => new Color(0.780f, 0.680f, 0.920f),
            _ => new Color(0.800f, 0.800f, 0.800f)
        };
    }
}