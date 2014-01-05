var Raph = false;

function Update () {
}

function OnTriggerEnter(otherObj: Collider)
{
    if (otherObj.tag == "Player" && !Raph)
    {
        audio.Play();
        Raph=true;
    } 
    else {
        audio.Stop();
    }
}