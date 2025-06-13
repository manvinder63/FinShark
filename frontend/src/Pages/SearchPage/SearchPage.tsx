import { ChangeEvent, SyntheticEvent, useState } from 'react';
import { CompanySearch } from '../../company';
import { searchCompanies } from '../../api';
import Navbar from '../../Components/Navbar/Navbar';
import Search from '../../Components/Search/Search';
import ListPortfolio from '../../Portfolio/ListPortfolio/ListPortfolio';
import CardList from '../../Components/CardList/CardList';



interface Props {}

const SearchPage = (props: Props) => {

    const [search, setSearch] =useState<string>("");
  const [searchResult, setSearchResult] =  useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);


  const handleChange = (e:ChangeEvent<HTMLInputElement>) => {
     setSearch(e.target.value);
  }
 
  const handleButtonClick = async (e: SyntheticEvent) => {
   e.preventDefault();
     const result = await searchCompanies(search);
      console.log(result);
     if(typeof result === "string"){
        setServerError(result);
     }
     else if(Array.isArray(result.data))
     {
        setSearchResult(result.data);
     }

  }

  const onPortfolioCreate = (e: any) => {
   e.preventDefault();

   const exists = portfolioValues.find((portfolioVal) => portfolioVal == e.target[0].value);
   if(exists) return
   const tempPortfolioValues = [
      ...portfolioValues,
      e.target[0].value
   ]

   setPortfolioValues(tempPortfolioValues);

  }

  const onDeletePortfolioItems = (e: any) => {
         e.preventDefault();
         const filteredPortfolio = portfolioValues.filter((value) => {
            return value !== e.target[0].value;
         });

         setPortfolioValues(filteredPortfolio);
  }

  return (
     <>
        <Search search={search} handleChange={handleChange} handleButton={handleButtonClick} />
        <ListPortfolio portfolioValues={portfolioValues} onDeleteClick={onDeletePortfolioItems}/>
        <CardList searchResult={searchResult} onPortfolioCreate={onPortfolioCreate}/>
        
        {serverError && <div>Unable to connect to API!</div>}
     </>
  )
}

export default SearchPage