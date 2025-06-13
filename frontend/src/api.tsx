import axios from "axios";
import { CompanyKeyMetrics, CompanyProfile, CompanySearch } from "./company";

interface SearchResponse {
    data: CompanySearch[];
}

export const searchCompanies = async (query: string) => {
        try {
            const data = await axios.get<SearchResponse>(
                `https://financialmodelingprep.com/api/v3/search-ticker?query=AA&limit=10&exchange=NASDAQ&apikey=${import.meta.env.VITE_API_KEY}`
            );

            return data;
        }
        catch (error) {
            if(axios.isAxiosError(error)) {
                console.log("error message: ", error.message);
                return error.message;
            } else {
                console.log("unexpected error: ", error);
                return "An unexpected error has occured."
            }
        }
}

export const getCompanyProfile = async (query: string) => {

    try{
        const data = await axios.get<CompanyProfile[]>(
                 `https://financialmodelingprep.com/api/v3/profile/${query}?apikey=${import.meta.env.VITE_API_KEY}`
        );

        return data;
    }
    catch(error:any) {
        console.log(error)
    }

}


export const getKeyMetrics = async (query: string) => {

    try{
        const data = await axios.get<CompanyKeyMetrics[]>(
                 `https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?apikey=${import.meta.env.VITE_API_KEY}`
        );
        return data;
    }
    catch(error:any) {
        console.log(error)
    }
}
