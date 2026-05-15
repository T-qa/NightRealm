namespace Qanht.NightRealm.AbilitySystem
{
    public interface IEffect
    {
        EffectInfo EffectInfo { get; }
        void Instanciate(Fighter source, Fighter target);
        void CleanUp();
    }
}
