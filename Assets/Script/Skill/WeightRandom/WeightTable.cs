using UnityEngine;


[CreateAssetMenu(fileName = "NewWeightedTable", menuName = "Game/Weighted Table")]
public class WeightedTable : ScriptableObject
{
    public WeightedGameObject[] objects;

    public string GetRandom()
    {
        float totalWeight = 0f;
        foreach (var obj in objects)//媛以묒튂??珥앺빀??怨꾩궛,媛??ㅻ툕?앺듃?ㅼ? ?쒖꽌?濡?媛以묒튂 ?먮━瑜?媛吏?
        {
            totalWeight += obj.weight; 
        }

        float rand = Random.Range(0f, totalWeight);//珥?媛以묒튂?먯꽌 ?쒕뜡媛믪쓣 戮묒쓬
        float current = 0f;

        foreach (var obj in objects)//?쒖감?곸쑝濡??ㅻ툕?앺듃?ㅼ쓽 媛以묒튂瑜??뷀빐二쇰㈃???쒕뜡媛믩낫??珥덇낵?덉쓣???대떦 ?ㅻ툕?앺듃瑜?諛섑솚
        {
            current += obj.weight;
            if (rand < current)
            {
                return obj.Serialname;
            }
        }

        return null;
    }
}
