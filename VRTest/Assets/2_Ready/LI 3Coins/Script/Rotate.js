var Speed : float;
function Update() {

    transform.Rotate(Vector3.up * Time.deltaTime*Speed);

}