using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class TargetArrowInstaller : MonoInstaller
{
    [SerializeField]
    private Sprite[] _sprites;

    public override void InstallBindings()
    {
        Container.BindInstance(GetComponent<Image>());
        Container.BindInstance(GetComponent<UILerpComponent>());
        Container.BindInstance(_sprites);
        Container.BindInstance(GetComponent<RectTransform>());

        TargetSelectionArrow selectionArrow = GetComponent<TargetSelectionArrow>();
        if (selectionArrow != null)
        {
            Container.BindInstance(selectionArrow);
        }
    }	
}