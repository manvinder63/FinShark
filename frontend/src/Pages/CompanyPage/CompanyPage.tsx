import { useParams } from "react-router"
import { getCompanyProfile } from "../../api";
import { useEffect, useState } from "react";
import { CompanyProfile } from "../../company";
import Sidebar from "../../Components/Sidebar/Sidebar";
import CompanyDashboard from "../../Components/CompanyDashboard/CompanyDashboard";
import Tile from "../../Components/Tile/Tile";

interface Props {}

const CompanyPage = (props: Props) => {

  let {ticker} = useParams();
  const [company, setCompany] = useState<CompanyProfile>();

  useEffect(() => {
     const getProfileInit = async () => {
      const result = await getCompanyProfile(ticker!);
      setCompany(result?.data[0]);
     }

     getProfileInit();
  }, [])
  
  return (
    <>
      {
        company ? (
          <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">

          <Sidebar />

          <CompanyDashboard ticker={ticker!}>
            <Tile title="Company Name" subTitle={company.companyName} />
          </CompanyDashboard>

        </div>
        ) : (
          <div>Company not found!</div>
        )
      }
    </>
  )
}

export default CompanyPage