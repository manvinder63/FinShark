import { SyntheticEvent } from "react"
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio"
import { Link } from "react-router-dom"

interface Props {
    portfolioValue: string
    onDeleteClick : (e: SyntheticEvent) => void
}

const CardPortfolio = ({ portfolioValue, onDeleteClick}: Props) => {
  return (
    <>
        <Link to={`/coompany/${portfolioValue}`}>{portfolioValue} </Link>
        <DeletePortfolio portfolioValue={portfolioValue} onDeleteClick={onDeleteClick} />
    </>
  )
}

export default CardPortfolio