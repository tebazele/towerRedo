import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class CommentsService {
    async delete(id) {
       const res = await api.delete(`api/comments/${id}`)
       logger.log(res.data, 'deleting comment')
       AppState.comments = AppState.comments.filter(c => c.id != id)
    }

}

export const commentsService = new CommentsService()