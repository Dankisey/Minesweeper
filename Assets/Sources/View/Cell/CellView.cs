using UnityEngine;
using Sapper.Model;

[RequireComponent(typeof(CellViewData))]
[RequireComponent(typeof(Animator))]
public abstract class CellView : MonoBehaviour
{
    private const string Flagged = nameof(Flagged);
    private const string Reveal = nameof(Reveal);
    private Animator _animator;

    protected CellViewData Data;

    public virtual void Init(Cell cell)
    {
        _animator = GetComponent<Animator>();
        Data = GetComponent<CellViewData>();
        CellModel = cell;

        CellModel.Opened += OnOpened;
        CellModel.FlagStatusChanged += OnFlagStatusChanged;
    }

    public Cell CellModel { get; private set; }

    protected void ForceOpen()
    {
        _animator.SetTrigger(Reveal);
    }

    protected void ForceUnflag()
    {
        _animator.SetBool(Flagged, false);
    }

    private void OnOpened(Cell cell)
    {
        _animator.SetTrigger(Reveal);
    }

    public void OnFlagStatusChanged(bool isFlagged)
    {
        _animator.SetBool(Flagged, isFlagged);
    }
}