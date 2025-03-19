package models;

public class CreateSessionModel
{
    private String vectorDbName;

    public String getVectorDbName() {
        return vectorDbName;
    }

    public void setVectorDbName(String vectorDbName) {
        this.vectorDbName = vectorDbName;
    }

    public CreateSessionModel(String _vectorDbName)
    {
        vectorDbName = _vectorDbName;
    }

}

