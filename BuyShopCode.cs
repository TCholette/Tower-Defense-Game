using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyShopCode : MonoBehaviour { 

    public Text craftText;
 
    public int baseMaterialCost;

    public Text materialCost;

    public GameObject Player;

    private int amountBought;

    private void Start() {

      
            materialCost.text = baseMaterialCost + "";
       
}

    // Start is called before the first frame update
    public void Craft() {


        if (Craftable()) {
            //GetComponent<Animator>().SetTrigger("crafted");
            craftText.text = "" + (int.Parse(craftText.text) + 1);

            
                Player.GetComponent<levelsCode>().money = (Player.GetComponent<levelsCode>().money - int.Parse(materialCost.text));
                

              

                //augment price
                materialCost.text = "" + (int.Parse(materialCost.text) + baseMaterialCost/10 + 1+ amountBought);
                

                amountBought++;


            

        } //else {
           // GetComponent<Animator>().SetTrigger("failed");
        //}
    }

    private bool Craftable() {
     
            if (!(Player.GetComponent<levelsCode>().money >= int.Parse(materialCost.text))) {
                return false;
            }

        
        return true;
    }
}
